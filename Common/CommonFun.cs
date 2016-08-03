using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;
using System.IO;

namespace PromolinkUS.Common
{
    public class CommonFun
    {
        public CommonFun()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string showObject(object o)
        {
            if (o != null)
            {
                return o.ToString();
            }
            else
            {
                return "";
            }
        }
        public class ConstantInfo
        {
            public ConstantInfo()
            {
            }
            public ConstantInfo(string _constantValue, string _constantText)
            {
                this.ConstantValue = _constantValue;
                this.ConstantText = _constantText;
            }
            public ConstantInfo(string _constantValue, string _constantText, string _constantText2)
            {
                this.ConstantValue = _constantValue;
                this.ConstantText = _constantText;
                this.constantText2 = _constantText2;
            }
            private string constantValue;
            private string constantText;
            private string constantText2;
            public string ConstantValue
            {
                get
                {
                    return constantValue;
                }
                set
                {
                    this.constantValue = value;
                }
            }
            public string ConstantText
            {
                get
                {
                    return constantText;
                }
                set
                {
                    this.constantText = value;
                }
            }
            public string ConstantText2
            {
                get
                {
                    return constantText2;
                }
                set
                {
                    this.constantText2 = value;
                }
            }
        }
        public static bool validateZipcode(string zipcode)
        {
            bool result = false;
            Regex rx = null;
            Match m = null;
            rx = new Regex(@"\d{5}(-\d{4})?", RegexOptions.IgnoreCase);
            m = rx.Match(zipcode);
            if (m.Success)
            {
                result = true;
            }
            return result;
        }

        public static string GetNumber(string _str)
        {
            string number_str = "";
            foreach (char c in _str)
            {
                if (char.IsNumber(c))
                {
                    number_str = number_str + c.ToString().Trim();
                }
            }
            return number_str;
        }

        public static string formatPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return "";
            }
            phone = GetNumber(phone);
            if (phone.Length >= 10)
            {
                phone = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6, 4);
            }
            return phone;
        }

        public static string IsStringNull(string obj)
        {
            if (obj == "" || obj == null)
            {
                return "";
            }
            else
            {
                return obj;
            }
        }

        public static bool IsNumber(string strNumber)
        {
            return new Regex(@"^\d+$").IsMatch(strNumber);
        }

        public static bool IsEmail(string strNumber)
        {
            return new Regex(@"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$").IsMatch(strNumber);
        }

        public static bool IsFloat(string strNumber)
        {
            return new Regex(@"^\d+\.{0,1}\d*$").IsMatch(strNumber);
        }

        public static DateTime GetWeekDate(DateTime time, DayOfWeek week)
        {
            DateTime monday = time;
            switch (time.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    monday = time;
                    break;
                case DayOfWeek.Tuesday:
                    monday = time.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    monday = time.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    monday = time.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    monday = time.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    monday = time.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    monday = time.AddDays(-6);
                    break;
            }

            DateTime result = monday;
            switch (week)
            {
                case DayOfWeek.Monday:
                    result = monday;
                    break;
                case DayOfWeek.Tuesday:
                    result = monday.AddDays(1);
                    break;
                case DayOfWeek.Wednesday:
                    result = monday.AddDays(2);
                    break;
                case DayOfWeek.Thursday:
                    result = monday.AddDays(3);
                    break;
                case DayOfWeek.Friday:
                    result = monday.AddDays(4);
                    break;
                case DayOfWeek.Saturday:
                    result = monday.AddDays(5);
                    break;
                case DayOfWeek.Sunday:
                    result = monday.AddDays(6);
                    break;
            }
            return result;
        }

        

            

        public static string MapPath(string strPath)
        {
            strPath = strPath.Replace("/", "\\");
            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.TrimStart('\\');
            }
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        public static string CustomMonthText(int month)
        {
            string monthText = null;
            switch (month)
            {
                case 1:
                    monthText = "Jan";
                    break;
                case 2:
                    monthText = "Feb";
                    break;
                case 3:
                    monthText = "Mar";
                    break;
                case 4:
                    monthText = "Apr";
                    break;
                case 5:
                    monthText = "May";
                    break;
                case 6:
                    monthText = "Jun";
                    break;
                case 7:
                    monthText = "Jul";
                    break;
                case 8:
                    monthText = "Aug";
                    break;
                case 9:
                    monthText = "Spt";
                    break;
                case 10:
                    monthText = "Oct";
                    break;
                case 11:
                    monthText = "Nov";
                    break;
                case 12:
                    monthText = "Dec";
                    break;
            }
            return monthText;
        }

        public string DicideTheLastWeekOfTuesday()
        {
            DateTime dt = DateTime.Now;
            if (dt.DayOfWeek == DayOfWeek.Tuesday)
            {
                DateTime dtLastDayofMonth = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
                TimeSpan ts = dtLastDayofMonth - dt;
                if (ts.Days < 7)
                {
                    if (dt.Hour == 13)
                    {
                        return "pagShowLimitInfo.aspx";
                    }
                }
            }
            return "";
        }

        public static bool ChangeFileName(string srcRelativePath, string desRelativePath)
        {
            srcRelativePath = HttpContext.Current.Server.MapPath(srcRelativePath);
            desRelativePath = HttpContext.Current.Server.MapPath(desRelativePath);

            try
            {
                if (File.Exists(srcRelativePath))
                {
                    File.Move(srcRelativePath, desRelativePath);
                    return true;
                }
                else
                    return false;
            }
            catch {
                return false;
            }
        }
    }
}
