using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PromolinkUS.Common
{
    public static class ExtensionString
    {
        public static string ReplaceAsCommaStr(this String s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace(";", ",");
                s = s.Replace(", ", ",");
                s = s.Replace(' ', ',');
                s = s.Replace('\t', ',');
                s = s.Replace('\r', ',');
                s = s.Replace('\n', ',');
                while (s.IndexOf(",,") > 0)
                {
                    s = s.Replace(",,", ",");
                }
                if (s.LastIndexOf(",") == s.Length-1)
                {
                    s = s.Substring(0, s.Length - 1);                
                }
            }
            else
            {
                s = "";
            }
            return s;
        }

        public static Int32 ToInt32(this Int32? s)
        {
            return ToInt32(Convert.ToString(s), 0);
        }
        public static Int32 ToInt32(this String s)
        {
            return ToInt32(s, 0);
        }
        public static Int32 ToInt32(this String s, Int32 _defaultValue)
        {
            int r = _defaultValue;
            if (s != null)
            {
                Int32.TryParse(s, out r);
            }
            return r;
        }
        public static Int64 ToInt32(this Int64? s)
        {
            return ToInt64(Convert.ToString(s), 0);
        }
        public static Int64 ToInt64(this String s)
        {
            return ToInt64(s, 0);
        }
        public static Int64 ToInt64(this String s, Int64 _defaultValue)
        {
            Int64 r = _defaultValue;
            if (s != null)
            {
                Int64.TryParse(s, out r);
            }
            return r;
        }
        public static Double ToDouble(this Double? s)
        {
            return ToDouble(Convert.ToString(s), 0.0);
        }
        public static Double ToDouble(this String s)
        {
            return ToDouble(s, 0.0);
        }
        public static Double ToDouble(this String s, Double _defaultValue)
        {
            Double r = _defaultValue;
            if (s != null)
            {
                Double.TryParse(s, out r);
            }
            return r;
        }
        public static Decimal ToDecimal(this Decimal? s)
        {
            return ToDecimal(Convert.ToString(s), 0.0m);
        }
        public static Decimal ToDecimal(this String s)
        {
            return ToDecimal(s, 0.0m);
        }
        public static Decimal ToDecimal(this String s, Decimal _defaultValue)
        {
            Decimal r = _defaultValue;
            if (s != null)
            {
                Decimal.TryParse(s, out r);
            }
            return r;
        }

        public static DateTime? ToDateTime(this String s)
        {
            DateTime dt = DateTime.Now;
            if (DateTime.TryParse(s, out dt) == false)
            {
                return null;
            }
            return dt;
        }
        public static string ToShoutDateTime(this String s)
        {
            if (s == null)
            {
                return "";
            }
            return ToShoutDateTime(Convert.ToDateTime(s));
        }
        public static string ToShoutDateTime(this DateTime? dt)
        {
            if (dt == null)
            {
                return "";
            }
            return ToShoutDateTime(Convert.ToDateTime(dt));
        }
        public static string ToShoutDateTime(this DateTime dt)
        {
            DateTime tempdt = dt;
            if (tempdt != null)
            {
                return String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(tempdt));
            }
            else
            {
                return "";
            }
        }
        public static Boolean ToBoolean(this Boolean? s)
        {
            return ToBoolean(Convert.ToString(s));
        }
        public static Boolean ToBoolean(this String s)
        {
            if (String.IsNullOrEmpty(s) == false && (s == "1" || Convert.ToString(s).ToLower() == "true"))
            {
                return true;
            }
            return false;
        }
        public static Boolean IsValidEmail(this String s)
        {
            Regex rCode = new Regex(@"^\w+((-\w+)|(\.\w+))*\@\w+((\.|-)\w+)*\.\w+$");
            if (rCode.IsMatch(s) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static Boolean IsValidZip(this String s)
        {
            if (String.IsNullOrEmpty(s) == false && s.Trim().IsDigit() && s.Trim().Length == 5)
            {
                return true;
            }
            return false;
        }
        public static Boolean IsDigit(this String s)
        {
            Regex rCode = new Regex(@"^\d*$");
            if (string.IsNullOrEmpty(s) == true || rCode.IsMatch(s.Trim()) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static Boolean IsPostCode(this String s)
        {
            Regex rCode = new Regex(@"^[A-Za-z]\d[A-Za-z]\s+\d[A-Za-z]\d$|^[A-Za-z]\d[A-Za-z]$");
            if (string.IsNullOrEmpty(s) == true || rCode.IsMatch(s.Trim()) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string ReplaceTAB(this String s)
        {
            if (s == null)
            {
                return "";
            }
            return Regex.Replace(s, "\\n", "").Replace("\\t", "").Replace("\\r", "").Replace("\\v", "");
        }

        public static string RemoveOther(this String s)
        {
            if (s == null)
            {
                return "";
            }
            return Regex.Replace(s, "\n", "").Replace("\t", "").Replace("\r", "").Replace("\v", "");
        }
        public static int GetMaxNum(int a, int b, int c)
        {
            int y = a;

            if (y < b)
                y = b;
            if (y < c)
                y = c;
            return y;
        }
    }

    //public static class ContantsNameExtensions
    //{
    //    public static ToString()
    //}
}
