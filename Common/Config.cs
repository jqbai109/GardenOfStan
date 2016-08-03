using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromolinkUS.Common
{
    public class Config
    {
        public static string GetAppSettings(string key)
        {
            return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings[key]);
        }

        public static bool InOldPCS
        {

            get
            {
                bool val;
                if (GetAppSettings("InOldPCS") == "true")
                    val = true;
                else
                    val = false;


                return val;
            }
        }
        public static bool EmailTestMode
        {

            get
            {
                bool val;
                if (GetAppSettings("EmailTestMode") == "true")
                    val = true;
                else
                    val = false;


                return val;
            }
        }
        
        public static List<string> DevEMails
        {

            get
            {
                string val=GetAppSettings("DevEMails");
                List<string> list = val.Split(';').ToList<string>();
                for (int i = 0; i < list.Count; i++ )
                {
                    if (CommonFun.IsEmail(list[i]) == false)
                    {
                        list.Remove(list[i]);
                    }
                }
                if (list.Count == 0)
                {
                    list.Add("swang@bridgetree.com");
                    list.Add("tdong@bridgetree.com");
                    list.Add("tyang@bridgetree.com");
                    list.Add("jshi@bridgetree.com");
                }
                return list;
            }
        }

        public static string PhysicalApplicationPath
        {

            get
            {
                string val = GetAppSettings("PhysicalApplicationPath");
                if (!string.IsNullOrEmpty(val))
                { }
                else {
                    val = "E:\\wwwroot\\LowesPromolinkUSMVC_Service\\";
                }

                return val;
            }
        }

        public static int RegularPageSize
        {
            get
            {
                string val=GetAppSettings("RegularPageSize");
                if (CommonFun.IsNumber(val)==true)
                {
                    return Convert.ToInt32(val);
                }                 
                return 10;
            }
        }
        public static int ReportPageSize
        {
            get
            {
                string val = GetAppSettings("ReportPageSize");
                if (CommonFun.IsNumber(val) == true)
                {
                    return Convert.ToInt32(val);
                }
                return 10;
            }
        }
        public static bool EmailNeedSend
        {
            get
            {
                string val = GetAppSettings("EmailNeedSend");
                if (val=="true")
                {
                    return true;
                }                 
                return false;
            }
        }
        public static string FromEmail
        {
            get
            {
              return  GetAppSettings("FromEmail");
            }
        }
        public static string FromName
        {
            get
            {
                return GetAppSettings("FromName");
            }
        }
        public static bool IsTest
        {
            get
            {
                string val = GetAppSettings("IsTest");
                if (val == "true")
                {
                    return true;
                }
                return false;
            }
        }

    }
}
