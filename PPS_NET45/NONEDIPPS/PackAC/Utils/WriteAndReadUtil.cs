using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PackListAC.Entitys;


namespace PackListAC.Utils
{
    class WriteAndReadUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileBackPath">A path</param>
        /// <param name="filePath">B path</param>
        /// <param name="fileContent"> 文件内容</param>
        /// <returns></returns>
        public static ExecuteResult writeToByFilePathAndFileContent(string fileBackUpPath, string filePath, string fileContent)
        {
            ExecuteResult exeRes = new ExecuteResult();
            try
            {
                if (File.Exists(fileBackUpPath))
                {
                    File.Delete(fileBackUpPath);
                }
                FileStream fs = new FileStream(fileBackUpPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                byte[] mybyte = Encoding.UTF8.GetBytes(fileContent);
                fileContent = Encoding.UTF8.GetString(mybyte);
                sw.Write(fileContent);
                sw.Close();
                File.Copy(fileBackUpPath, filePath, true);
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }

        /// <summary>
        /// 备份并抛转文件
        /// </summary>
        /// <param name="fileBackUpPath">备份路径</param>
        /// <param name="lisfilePath">目的路径集合</param>
        /// <param name="fileContent">文件内容</param>
        /// <returns>执行结果</returns>
        public static ExecuteResult writeToByFilePathAndFileContent(string fileBackUpPath, List<string> lisfilePath, string fileContent)
        {
            
            ExecuteResult exeRes = new ExecuteResult();
            try
            {
                if (File.Exists(fileBackUpPath))
                {
                    File.Delete(fileBackUpPath);
                }
                FileStream fs = new FileStream(fileBackUpPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                byte[] mybyte = Encoding.UTF8.GetBytes(fileContent);
                fileContent = Encoding.UTF8.GetString(mybyte);
                sw.Write(fileContent);
                sw.Close();
                //遍历目的路径并写入文件
                int check = lisfilePath.Count;
                foreach (string filePath in lisfilePath)
                {
                    //File.Copy(fileBackUpPath, filePath, true);
                    try
                    {
                        File.Copy(fileBackUpPath, filePath, true);
                    }

                    catch (Exception e)
                    {
                        check--;
                        if (check == 0)
                        {
                            throw;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }
    }
}
