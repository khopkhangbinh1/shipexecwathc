using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Data.OracleClient;
using System.Xml;
using System.Drawing;

namespace SajetClass
{
    class SajetCommon
    {
        public static string g_sFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();  //�ɮת��� 
        public static string g_sFileName = Path.GetFileName(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName); //�ɮצW��          
        public static string g_sExeName = ClientUtils.fCurrentProject;

        public static DialogResult Show_Message(string sKeyMsg, int iType)
        {
            string sXMLFile = Path.GetFileNameWithoutExtension(g_sFileName);
            return ClientUtils.ShowMessage(SetLanguage(sKeyMsg), iType, g_sExeName, sXMLFile);
        }

        public static string SetLanguage(string sSearchText, string sDefaultTxt, int iTransType)
        {
            string sText = SetLanguage(sSearchText, iTransType);
            if (sText != sSearchText)
                return sText;
            else
                return sDefaultTxt;
        }
        public static string SetLanguage(string sSearchText)
        {
            string sXMLFile = "";
            string sText = "";

            sXMLFile = Path.GetFileNameWithoutExtension(g_sFileName); //Dll.xml
            sText = ClientUtils.SetLanguage(sSearchText, g_sExeName, sXMLFile);
            if (sSearchText == sText)
            {
                sXMLFile = g_sExeName; //Program.xml
                sText = ClientUtils.SetLanguage(sSearchText, g_sExeName, sXMLFile);
            }           
            return sText;
        }
        public static string SetLanguage(string sSearchText, int iTransType)
        {
            string sXMLFile = "";
            switch (iTransType)
            {
                case 1:  //Dll.xml
                    sXMLFile = Path.GetFileNameWithoutExtension(g_sFileName);
                    break;
                case 2:  //Program.xml
                    sXMLFile = g_sExeName;
                    break;
            }
            string sText = ClientUtils.SetLanguage(sSearchText, g_sExeName, sXMLFile);
            return sText;
        }
        public static void SetLanguageControl(Control c)
        {
            //�ഫ����Txt���h��y��
            string sXMLFile = Path.GetFileNameWithoutExtension(g_sFileName);
            ClientUtils.SetLanguage(c, g_sExeName, sXMLFile);
            ClientUtils.SetLanguage(c, g_sExeName, g_sExeName);
        }

        public static string GetSysBaseData(string sProgram, string sParamName, ref string sErrorMsg)
        {
            //Ū��SYS_BASE�]�w��
            string sSQL = "";
            sSQL = " SELECT PARAM_VALUE "
                 + "   FROM SAJET.SYS_BASE "
                 + "  WHERE Upper(PROGRAM) = '" + sProgram.ToUpper() + "' "
                 + "    and Upper(PARAM_NAME) = '" + sParamName.ToUpper() + "' ";
            DataSet ds = ClientUtils.ExecuteSQL(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["PARAM_VALUE"].ToString();
            else
            {
                sErrorMsg = sErrorMsg + sParamName + Environment.NewLine;
                return "";
            }
        }

        public static string GetMaxID(string sTable, string sField, int iIDLength)
        {
            string sMaxID = "0";
            try
            {
                object[][] Params = new object[5][];
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TFIELD", sField };
                Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TTABLE", sTable };
                Params[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TNUM", iIDLength.ToString() };
                Params[3] = new object[] { ParameterDirection.Output, OracleType.VarChar, "TRES", "" };
                Params[4] = new object[] { ParameterDirection.Output, OracleType.VarChar, "T_MAXID", "" };
                DataSet ds = ClientUtils.ExecuteProc("SAJET.SJ_GET_MAXID", Params);

                string sRes = ds.Tables[0].Rows[0]["TRES"].ToString();
                sMaxID = ds.Tables[0].Rows[0]["T_MAXID"].ToString();

                if (sRes != "OK")
                {
                    SajetCommon.Show_Message(sRes, 0);
                    return "0";
                }
            }
            catch (Exception ex)
            {
                SajetCommon.Show_Message("SAJET.SJ_GET_MAXID" + Environment.NewLine + ex.Message, 0);
                return "0";
            }
            return sMaxID;
        }

        public static string GetID(string sTable, string sFieldID, string sFieldName, string sValue)
        {
            return GetID(sTable, sFieldID, sFieldName, sValue, "");
        }
        public static string GetID(string sTable, string sFieldID, string sFieldName, string sValue,string sEnabled)
        {
            //�����ID��
            if (string.IsNullOrEmpty(sValue))
                return "0";
            string sSQL = "select " + sFieldID + " from " + sTable + " "
                        + "where " + sFieldName + " = '" + sValue + "' ";
            if (!string.IsNullOrEmpty(sEnabled))
                sSQL = sSQL + " and ENABLED = '" + sEnabled + "' ";
            sSQL = sSQL + " and Rownum = 1 ";

            DataSet ds = ClientUtils.ExecuteSQL(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][sFieldID].ToString();
            else
                return "0";
        }

        public static Image LoadImage(string sFileName)
        {
            string sPath = Application.StartupPath + "\\";
            if (File.Exists(sPath + sFileName))
                return Image.FromFile(sPath + sFileName);
            else
                return null;
        }

        /// <summary>
        /// ��INI�ļ������ݶ�ȡ��DataTable��
        /// </summary>
        /// <param name="fileName">INI�ļ�·��</param>
        /// <returns>���ض�ȡ��INI���ݵ�DataTable</returns>
        public static DataTable OpenINI(string filePath)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("GBK"));//����������⣬֮ǰΪUTF8  Update by  Mingtian 2015/10/14/19��20
            //��¼ÿ�м�¼�еĸ��ֶ�����
            string[] aryLine = null;
            string[] tableHead = null;
            string sLine = "";
            string sTableHead = "";
            //��ʾ����
            int columnCount = 0;
            //��ʾ�Ƿ��Ƕ�ȡ�ĵ�һ��
            bool IsStatus = false;
            string s = "";

            while ((s = sr.ReadLine()) != null)
            {
                if (s.IndexOf("=") >= 0)
                {
                    sTableHead += s.Substring(0, s.IndexOf("=")) + ",";
                    sLine += s.Substring(s.IndexOf("=") + 1) + ",";
                    IsStatus = true;
                }
            }
            if (IsStatus = true)
            {
                tableHead = sTableHead.Split(',');
                columnCount = tableHead.Length;
                //������
                for (int i = 0; i < columnCount - 1; i++)
                {
                    DataColumn dc = new DataColumn(tableHead[i]);
                    dt.Columns.Add(dc);
                }

                aryLine = sLine.Split(',');
                DataRow dr = dt.NewRow();
                for (int j = 0; j < columnCount - 1; j++)
                {
                    dr[j] = aryLine[j];
                }
                dt.Rows.Add(dr);
            }
            sr.Close();
            fs.Close();
            return dt;
        }

        /// <summary>
        /// ��CSV�ļ������ݶ�ȡ��DataTable��
        /// </summary>
        /// <param name="fileName">CSV�ļ�·��</param>
        /// <returns>���ض�ȡ��CSV���ݵ�DataTable</returns>
        public static DataTable OpenCSV(string filePath)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //��¼ÿ�ζ�ȡ��һ�м�¼
            string strLine = "";
            //��¼ÿ�м�¼�еĸ��ֶ�����
            string[] aryLine = null;
            string[] tableHead = null;
            //��ʾ����
            int columnCount = 0;
            //��ʾ�Ƿ��Ƕ�ȡ�ĵ�һ��
            bool IsFirst = true;
            //���ж�ȡCSV�е�����
            while ((strLine = sr.ReadLine()) != null)
            {
                //strLine = Common.ConvertStringUTF8(strLine, encoding);
                //strLine = Common.ConvertStringUTF8(strLine);
                if (strLine.Substring(0,1) == ";")
                {
                    continue;
                }
                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //������
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }

            sr.Close();
            fs.Close();
            return dt;
        }

        //��Sajet_Log
        public static void SaveLog(string sStaus, string sMessage)
        {
            //ץȡ�·�
            string sMonth = "";
            if (DateTime.Now.Month.ToString().Length > 1)
            {
                sMonth = DateTime.Now.Month.ToString();
            }
            else
            {
                sMonth = "0" + DateTime.Now.Month.ToString();
            }
            //ץȡ����
            string sDay = "";
            if (DateTime.Now.Month.ToString().Length > 1)
            {
                sDay = DateTime.Now.Day.ToString();
            }
            else
            {
                sDay = "0" + DateTime.Now.Day.ToString();
            }

            //�řn��
            string sLogName = DateTime.Today.Year + "_" + sMonth + "_" + sDay + ".log";

            //�Д�Sajet_Log�Ƿ����
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\Sajet_Log"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Sajet_Log");
            }

            //�Д�sLogName�Ƿ����
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\Sajet_Log\" + sLogName))
            {
                //File.Create(Directory.GetCurrentDirectory() + @"\Sajet_Log\" + sLogName);


                FileStream fs = new FileStream(Directory.GetCurrentDirectory() + @"\Sajet_Log\" + sLogName, FileMode.Create, FileAccess.Write);//����д���ļ�

                //��ӍϢ
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(DateTime.Now.ToString() + sStaus + sMessage + "\r\n");

                //sw.AutoFlush = true;
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            else
            {

                FileStream fs = new FileStream(Directory.GetCurrentDirectory() + @"\Sajet_Log\" + sLogName, FileMode.Append, FileAccess.Write);//����д���ļ�
                //��ӍϢ
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(DateTime.Now.ToString() + sStaus + sMessage + "\r\n");

                //sw.AutoFlush = true;
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }
    }
}
