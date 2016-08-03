using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PromolinkUS.Common
{
    public class Logs
    {
        public void logs(string strLogs)
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd");
            logs(strLogs, fileName);
        }
        public void logs(string strLogs, string strfileName)
        {
            if (string.IsNullOrEmpty(strfileName) == true)
            {
                logs(strLogs);
                return;
            }

            if (Directory.Exists(MapPath("logs")) == false)
            {
                Directory.CreateDirectory(MapPath("logs"));
            }

            string fileName = MapPath(String.Format("logs/{0}.txt", strfileName));

            if (!File.Exists(fileName))
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine(strLogs);
                }
            }

            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(strLogs);
            }

        }

        public string MapPath(string strPath)
        {
            strPath = strPath.Replace("/", "\\");
            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.TrimStart('\\');
            }
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }
    }
}
