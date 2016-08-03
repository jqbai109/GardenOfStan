using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;

namespace PromolinkUS.Common
{
    public class BTRequest
    {
        public static string GetQueryString(string strName)
        {
            string strValue = HttpContext.Current.Request.QueryString[strName];
            if (strValue == null)
            {
                return "";
            }
            return strValue.Trim();
        }

        public static string GetPostString(string strName)
        {
            string strValue = HttpContext.Current.Request.Form[strName];
            if (strValue == null)
            {
                return "";
            }
            return strValue.Trim();
        }

        public static string GetString(string strName)
        {
            string strValue = HttpContext.Current.Request[strName];
            if (strValue == null)
            {
                return "";
            }
            return strValue.Trim();
        }

        public static int GetQueryInt(string strName)
        {
            return int.Parse(GetQueryString(strName));
        }

        public static string RawURL
        {
            get { return HttpContext.Current.Request.RawUrl; }
        }

        #region cookie session
        public static string GetSession(string SessionName)
        {
            if (HttpContext.Current != null)
            {
                if (null != HttpContext.Current.Session[SessionName])
                {
                    return Convert.ToString(HttpContext.Current.Session[SessionName]);
                }
            }

            return null;
        }
        //public static object GetSession(string SessionName)
        //{
        //    if (null != HttpContext.Current.Session[SessionName])
        //        return HttpContext.Current.Session[SessionName];
        //    else
        //        return null;
        //}

        public static void SetSession(string SessionName, object value)
        {
            if (null != HttpContext.Current.Session[SessionName])
                HttpContext.Current.Session[SessionName] = value;
            else
                HttpContext.Current.Session.Add(SessionName, value);
        }

        public static void SetCookies(string cookiesName, string value)
        {
            if (null != HttpContext.Current.Request.Cookies[cookiesName])
                HttpContext.Current.Response.Cookies[cookiesName].Value = value;
            else
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(cookiesName, value));
        }

        public static void SetCookies(string cookiesName, string value, DateTime expiresTime)
        {
            if (null != HttpContext.Current.Request.Cookies[cookiesName])
                HttpContext.Current.Response.Cookies[cookiesName].Value = value;
            else
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(cookiesName, value));
            HttpContext.Current.Response.Cookies[cookiesName].Expires = expiresTime;
        }

        public static void SetCookies(string cookiesmain, string cookiesName, string value)
        {
            if (null != HttpContext.Current.Request.Cookies[cookiesmain] && null != HttpContext.Current.Request.Cookies[cookiesmain][cookiesName])
                HttpContext.Current.Request.Cookies[cookiesmain][cookiesName] = value;
            else
                HttpContext.Current.Response.Cookies[cookiesmain].Values.Add(cookiesName, value);
        }

        public static void ClearCookies(string cookiesName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiesName];
            if (cookie != null)
            {
                cookie.Value = null;
                cookie.Expires = DateTime.Now.AddDays(-1.0);
                cookie.Values.Clear();
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

        public static string GetCookies(string cookiesName)
        {
            if (null != HttpContext.Current.Request.Cookies[cookiesName])
                return HttpContext.Current.Request.Cookies[cookiesName].Value;
            else
                return "";
        }

        public static string GetCookies(string cookiesmain, string cookiesName)
        {
            if (null != HttpContext.Current.Request.Cookies[cookiesmain] && null != HttpContext.Current.Request.Cookies[cookiesmain][cookiesName])
                return HttpContext.Current.Request.Cookies[cookiesmain][cookiesName].ToString();
            else
                return "";
        }
        #endregion



        public static string GetFileExtension(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) == false)
            {
                int dotIndex = filePath.LastIndexOf('.');
                if (dotIndex > -1)
                {
                    return filePath.Substring(dotIndex + 1);
                }
            }
            return "";
        }

        public static string c_formatedate(string p_date, int p_format)
        {
            string d;
            string n;
            string y;
            string r;
            if (p_date.ToDateTime() != null)
            {
                n = Convert.ToDateTime(p_date).Year.ToString();
                y = Convert.ToDateTime(p_date).Month.ToString();
                r = Convert.ToDateTime(p_date).Day.ToString();
                string formatedate = p_date;
                if (y.Length == 1)
                    y = "0" + y;
                if (r.Length == 1)
                    r = "0" + r;
                if (Convert.ToInt32(p_format) == 1)
                    formatedate = y + r + n;
                return formatedate;
            }
            else
            {
                return "";
            }

        }

        public static string AddSpanRed(string str)
        {
            return "<span style='color:red;'>" + str + "</span>";
        }

        public static object GetPropertyValue(object obj, string property) //linq to entity
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }

        public static List<T> IlistOrderBy<T>(List<T> list, string propertyName, string direction) where T : new() //entity
        {
            if((list == null) || list.Count == 0) return list;

            Type elementType = list[0].GetType();
            PropertyInfo propertyInfo = elementType.GetProperty(propertyName);

            ParameterExpression parameter = Expression.Parameter(elementType, "");
            Expression body = Expression.Property(parameter, propertyInfo);

            Expression sourceExpression = list.AsQueryable().Expression;

            Type sourcePropertyType = propertyInfo.PropertyType;

            if (!string.IsNullOrEmpty(direction) && direction == "desc")
                direction = "OrderByDescending";
            else
                direction = "OrderBy";

            Expression lambda = Expression.Call(
                typeof(Queryable), direction,
                new Type[] {elementType, sourcePropertyType},
                sourceExpression, Expression.Lambda(body, parameter));

            return list.AsQueryable().Provider.CreateQuery<T>(lambda).ToList<T>();
        }

    }
}
