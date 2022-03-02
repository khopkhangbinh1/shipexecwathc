using Dapper;
using MESModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDb;

namespace MESDataLayer
{
    public interface IDbOperation
    {
        IEnumerable<T> Query<T>(string sql, object model);
        int Add<T>(T model);
        int Add<T>(IEnumerable<T> model);
        int InsertUpdate<T>(T model, List<string> keyfields);
        int InsertUpdate<T>(IEnumerable<T> model, List<string> keyfields);
        int DoTransaction(Dictionary<string, object> data);
        int DoTransaction(string sql, object data);
    }

    public class DbDapperExtension : OracleDb, IDbOperation
    {
        private string _conn { get; set; }
        public DbDapperExtension(string conn)
            : base(conn)
        {
            _conn = conn;
        }

        public IEnumerable<T> Query<T>(string sql, object model = null)
        {
            try
            {
                DbCon.Open();
                return DbCon.Query<T>(sql, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DbCon.Close();
            }
        }

        public int Add<T>(T model)
        {
            string sql = generateInsertSQL<T>();
            return Add<T>(new List<T> { model });
        }
        public int Add<T>(IEnumerable<T> model)
        {
            int i = 0;
            string sql = generateInsertSQL<T>();
            try
            {
                DbCon.Open();
                using (var tra = DbCon.BeginTransaction())
                {
                    try
                    {
                        i = DbCon.Execute(sql, model, tra);
                        tra.Commit();
                    }
                    catch (Exception ex)
                    {
                        tra.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DbCon.Close();
            }
            return i;
        }


        /// <summary>
        /// Dictionary<string,object>  sql,param
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public int DoTransaction(Dictionary<string, object> data)
        {
            int i = 0;
            try
            {
                DbCon.Open();
                using (var tra = DbCon.BeginTransaction())
                {
                    try
                    {
                        foreach (KeyValuePair<string, object> item in data)
                        {
                            if (isList(item.Value))
                            {
                                foreach (var it in (item.Value as IList))
                                {
                                    i = DbCon.Execute(item.Key, it, tra);
                                }
                            }
                            else
                            {
                                i = DbCon.Execute(item.Key, item.Value, tra);
                            }
                        }
                        tra.Commit();
                    }
                    catch (Exception ex)
                    {
                        tra.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DbCon.Close();
            }
            return i;
        }

        public int DoTransaction(string sql, object data)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add(sql, data);
            return DoTransaction(dict);
        }

        public int InsertUpdate<T>(T model, List<string> keyfields = null)
        {
            string sql = generateInsertUpdate<T>(keyfields);
            return DoTransaction(sql, model);
        }
        public int InsertUpdate<T>(IEnumerable<T> model, List<string> keyfields = null)
        {
            string sql = generateInsertUpdate<T>(keyfields);
            return DoTransaction(sql, model);
        }

        public int DoExtremeSpeedTransaction(Dictionary<string, object> data)
        {
            int i = 0;
            using (var con = new OracleConnection(_conn))
            {
                con.Open();
                using (var tra = con.BeginTransaction())
                {
                    try
                    {
                        foreach (KeyValuePair<string, object> item in data)
                        {
                            if (isList(item.Value))
                            {
                                i = executeBatch(item.Key, (item.Value as IList), tra, con);
                            }
                            else
                            {
                                i = con.Execute(item.Key, item.Value, tra);
                            }

                        }
                        var s = "xx";
                        tra.Commit();
                    }
                    catch (Exception ex)
                    {
                        tra.Rollback();
                        throw ex;
                    }
                }
            }
            return i;
        }

        public int DoExtremeSpeedTransactionByDbType(IEnumerable<SqlModel> data)
        {
            int i = 0;
            using (var con = new OracleConnection(_conn))
            {
                con.Open();
                using (var tra = con.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in data)
                        {
                            i = executeBatch(item.Sql, item.Model as List<Dictionary<string,object>> , item.DbMapping, tra, con);
                        }
                        var s = "xx";
                        tra.Commit();
                    }
                    catch (Exception ex)
                    {
                        tra.Rollback();
                        throw ex;
                    }
                }
            }
            return i;
        }



        private string generateInsertSQL<T>()
        {
            string table = getTableName<T>();
            var properties = getProperties<T>();
            string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                table, string.Join(",", properties),
                string.Join(",", properties.Select(x => ":" + x)));
            return sql;
        }
        private string generateInsertUpdate<T>(List<string> keyfields)
        {
            string tablename = getTableName<T>();
            var pks = keyfields == null || keyfields.Count() < 1 ?
                this.GetTablePK(tablename).Tables[0].AsEnumerable().Select(x => x[0].ToString()).ToList() : keyfields;
            var fileds = getProperties<T>();

            var upfields = fileds.Except(pks, StringComparer.CurrentCultureIgnoreCase);

            string restrict = string.Join("AND", pks.Select(x => string.Format("{0} =: {0}", x)));
            string update = "update set " + string.Join(" ,", upfields.Select(x => string.Format("{0} =: {0}", x)));
            string insert = string.Format("insert ({0}) values ({1})",
                  string.Join(" ,", fileds),
                    string.Join(" ,", fileds.Select(x => ":" + x))
                );


            string sql = string.Format(@"
                     Merge into {0} a using dual on ({1})
                     when matched then {2}
                     when not matched then {3}
                    ", tablename, restrict, update, insert);

            return sql;

        }

        private string getTableName<T>()
        {
            //Type type = typeof(T);
            //var tableNameAtt = type.GetCustomAttributes(typeof(TableNameAttribute), false);
            //if (tableNameAtt != null && tableNameAtt.Length > 0)
            //{
            //    return ((TableNameAttribute)tableNameAtt[0]).Value;
            //}
            return "";
        }

        private IEnumerable<string> getProperties(Type t)
        {
            var properties = t.GetProperties().Where(x => !x.Name.StartsWith("_") && !x.Name.StartsWith("x_"));
            return properties.Select(x => x.Name);
        }
        private IEnumerable<string> getProperties<T>()
        {
            var properties = typeof(T).GetProperties().Where(x => !x.Name.StartsWith("_") && !x.Name.StartsWith("x_"));
            return properties.Select(x => x.Name);
        }

        private int executeBatch(string sql, IList list, OracleTransaction tra, OracleConnection ora, int batchSize = 10240)
        {
            int ret = 0;

            if (list == null || list.Count < 1)
                ret = ora.Execute(sql, list, tra);
            else
            {
                var models = ilistToList(list);
                var props = models.First().GetType().GetProperties().Where(x => !x.Name.StartsWith("_") && !x.Name.StartsWith("x_"));

                int loopTimes = models.Count() / batchSize;
                if (models.Count() % batchSize > 0)
                    loopTimes++;
                for (int i = 0; i < loopTimes; i++)
                {
                    int takeSize = batchSize;
                    if (i * batchSize + batchSize > models.Count())
                        takeSize = models.Count() - i * batchSize;

                    using (var cmd = ora.CreateCommand())
                    {
                        cmd.Transaction = tra;
                        cmd.ArrayBindCount = takeSize;
                        cmd.CommandText = sql;
                        cmd.BindByName = true;

                        foreach (var prop in props)
                        {
                            int pos = sql.IndexOf(prop.Name, StringComparison.OrdinalIgnoreCase);
                            if (pos > 0)
                            {
                                var p = cmd.CreateParameter();
                                p.ParameterName = prop.Name;
                                var propertyname = prop.PropertyType.ToString().Split('.').Last().TrimEnd(']');
                                propertyname = string.IsNullOrEmpty(propertyname) ? "" : propertyname.ToLower();
                                // Clob Blob

                                var DbLlob = prop.GetCustomAttributes(typeof(DbLlobAttribute), false);
                                if (DbLlob != null && DbLlob.Length > 0)
                                {
                                    string dataType = ((DbLlobAttribute)DbLlob[0]).Value;
                                    switch (dataType)
                                    {
                                        case "XMLTYPE":
                                            p.OracleDbType = OracleDbType.XmlType;
                                            break;
                                        case "BLOB":
                                            p.OracleDbType = OracleDbType.Blob;
                                            break;
                                        case "BFILE":
                                            p.OracleDbType = OracleDbType.BFile;
                                            break;
                                        case "CLOB":
                                            p.OracleDbType = OracleDbType.Clob;
                                            break;
                                        case "NCLOB":
                                            p.OracleDbType = OracleDbType.NClob;
                                            break;
                                        default:
                                            p.DbType = DbType.String;
                                            break;
                                    }

                                    p.Value = models.Skip(i * batchSize).Take(takeSize).Select(x => props.Where(y => y.Name == prop.Name).Single().GetValue(x)).ToArray();
                                }
                                else
                                {
                                    switch (propertyname)
                                    {
                                        case "datetime":
                                            p.DbType = DbType.DateTime;
                                            break;
                                        case "decimal":
                                            p.DbType = DbType.Decimal;
                                            break;
                                        default:
                                            p.DbType = DbType.String;
                                            break;
                                    }
                                    p.Value = models.Skip(i * batchSize).Take(takeSize).Select(x => props.Where(y => y.Name == prop.Name).Single().GetValue(x)).ToArray();
                                }
                                cmd.Parameters.Add(p);
                            }
                        }
                        ret += cmd.ExecuteNonQuery();
                    }
                }
            }

            return ret;
        }

        private int executeBatch(string sql, List<Dictionary<string,object>> model, Dictionary<string, string> dbMap, OracleTransaction tra, OracleConnection ora, int batchSize = 10240)
        {
            dbMap = dbMap==null?new Dictionary<string,string>():dbMap;
            var dbMapList = dbMap.Select(x => new {
                Key = x.Key.ToLower(),
                Value = x.Value.ToUpper()
            });
            int ret = 0;
            var props = model.First().Select(x=>x.Key).Where(x => !x.StartsWith("_") && !x.StartsWith("x_"));

            int loopTimes = model.Count() / batchSize;
            if (model.Count() % batchSize > 0)
                loopTimes++;
            for (int i = 0; i < loopTimes; i++)
            {
                int takeSize = batchSize;
                if (i * batchSize + batchSize > model.Count())
                    takeSize = model.Count() - i * batchSize;

                using (var cmd = ora.CreateCommand())
                {
                    cmd.Transaction = tra;
                    cmd.ArrayBindCount = takeSize;
                    cmd.CommandText = sql;
                    cmd.BindByName = true;

                    var m = model.First();

                    foreach (var prop in props)
                    {
                        int pos = sql.IndexOf(prop, StringComparison.OrdinalIgnoreCase);
                        if (pos > 0)
                        {
                            var p = cmd.CreateParameter();
                            p.ParameterName = prop;

                            var propertyname = m[prop].GetType().ToString().Split('.').Last().TrimEnd(']');
                            propertyname = string.IsNullOrEmpty(propertyname) ? "" : propertyname.ToLower();
                            // Clob Blob
                            var DbLlob = dbMapList.FirstOrDefault(x => x.Key == propertyname);

                            if (DbLlob!= null)
                            {
                                string dataType = DbLlob.Value;
                                switch (dataType)
                                {
                                    case "XMLTYPE":
                                        p.OracleDbType = OracleDbType.XmlType;
                                        break;
                                    case "BLOB":
                                        p.OracleDbType = OracleDbType.Blob;
                                        break;
                                    case "BFILE":
                                        p.OracleDbType = OracleDbType.BFile;
                                        break;
                                    case "CLOB":
                                        p.OracleDbType = OracleDbType.Clob;
                                        break;
                                    case "NCLOB":
                                        p.OracleDbType = OracleDbType.NClob;
                                        break;
                                    default:
                                        p.DbType = DbType.String;
                                        break;
                                }

                                p.Value = model.Skip(i * batchSize).Take(takeSize)
                                    .Select(x => x[prop]).ToArray();
                            }
                            else
                            {
                                switch (propertyname)
                                {
                                    case "datetime":
                                        p.DbType = DbType.DateTime;
                                        break;
                                    case "decimal":
                                        p.DbType = DbType.Decimal;
                                        break;
                                    default:
                                        p.DbType = DbType.String;
                                        break;
                                }
                                p.Value = model.Skip(i * batchSize).Take(takeSize)
                                    .Select(x => x[prop]).ToArray();
                            }
                            cmd.Parameters.Add(p);
                        }
                    }
                    ret += cmd.ExecuteNonQuery();
                }
            }

            return ret;
        }


        private bool isList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }
        private List<object> ilistToList(IList list)
        {
            object[] array = new object[list.Count];
            list.CopyTo(array, 0);
            return array.ToList();
        }
    }
}
