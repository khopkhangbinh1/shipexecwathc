using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibHelper
{
    public partial class LogHelper
    {
        public static void InsertPPSExcuteSNLog(string strStation, string strPointDesc, string strSN)
        {
            strStation = strStation.ToUpper().Trim();
            strPointDesc = strPointDesc.Trim();
            strSN = strSN.Trim();
            if (string.IsNullOrEmpty(strStation)) { strStation = "PPS"; }
            if (string.IsNullOrEmpty(strPointDesc)) { return; }
            if (string.IsNullOrEmpty(strSN)) { return; }
            //PPS主目录
            string strMainProPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //PPS主目录 + log + station \ 日期.txt
            string strLogPath = Path.Combine(strMainProPath, "Log", strStation);
            //station 创建文件夹，只保留2日内的文件
            CreatePathDeleteFile(strLogPath, 2);
            //文件的内容  站+记录节点 +序号+时间 
            //20200909 yyyyMMdd -->yyyyMMddHH
            string strLogFileName = DateTime.Now.ToString("yyyyMMddHH") + ".txt";
            //当日追加
            string strLogContent = string.Format("{0:yyyy-MM-dd HH-mm-ss-ffff}", DateTime.Now) + "#" +strStation + "#" + strPointDesc + "#" + strSN + "#" +  System.Environment.NewLine;
            
            File.AppendAllText(Path.Combine(strLogPath, strLogFileName), strLogContent);

        }
        private static void CreatePathDeleteFile(string filePath, Int16 iDays)
        {
            if (!Directory.Exists(filePath))
            { Directory.CreateDirectory(filePath); }
            string[] sonFiles = Directory.GetFiles(filePath);
            foreach (string sf in sonFiles)
            {
                //2天前的删除
                if (DateTime.Compare(File.GetLastWriteTime(sf).AddDays(iDays), DateTime.Now) <= 0)
                {
                    File.Delete(sf);
                }
            }
            //string[] sonDirecs = Directory.GetDirectories(filePath);
            //foreach (string s in sonDirecs)
            //{
            //    DeleteFile(s);
            //}
            //string[] fileLeft = Directory.GetFiles(filePath);
            //string[] sonLeft = Directory.GetDirectories(filePath);
            //if ((sonLeft.Length == 0) && (fileLeft.Length == 0))
            //{
            //    Directory.Delete(filePath);
            //}
        }
     
    }
}
