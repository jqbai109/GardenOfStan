using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;


namespace PromolinkUS.Common
{
    public abstract class RecordLogs
    {
        public SirvaN.ExtLog4Net.IMyLog Iloger { get; set; }
        private string _strOriginalString;
        public string strOriginalString
        {
            get
            {
                if (String.IsNullOrEmpty(_strOriginalString) == true)
                {
                    if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Url != null && HttpContext.Current.Request.Url.OriginalString != null)
                    {
                        _strOriginalString = HttpContext.Current.Request.Url.OriginalString;
                    }
                    else
                    {
                        _strOriginalString = "";
                    }
                }
                return _strOriginalString;
            }
        }
        public abstract void InitIloger();

        public void Debug<T>(String UserId, String Action, T obj) where T : class
        {
            Debug(UserId, Action, obj, string.Empty);
        }
        public void Debug<T>(String UserId, String Action, T obj, string message) where T : class
        {
            if (Iloger != null && Iloger.IsDebugEnabled)
            {
                Iloger.Debug(strOriginalString, UserId, Action, obj, GetObjJson(obj, message));
            }
        }
        public void Debug(String UserId, String Action, string message)
        {
            if (Iloger != null && Iloger.IsDebugEnabled)
            {
                Iloger.Debug(strOriginalString, UserId, Action, message);
            }
        }


        public void Info<T>(String UserId, String Action, T obj) where T : class
        {
            Info(UserId, Action, obj, string.Empty);
        }
        public void Info<T>(String UserId, String Action, T obj, string message) where T : class
        {
            if (Iloger != null && Iloger.IsInfoEnabled)
            {
                Iloger.Info(strOriginalString, UserId, Action, GetObjJson(obj, message));
            }
        }
        public void Info(String UserId, String Action, string message)
        {
            if (Iloger != null && Iloger.IsInfoEnabled)
            {
                Iloger.Info(strOriginalString, UserId, Action, message);
            }
        }

        public void Warn<T>(String UserId, String Action, T obj) where T : class
        {
            Warn(UserId, Action, obj, string.Empty);
        }
        public void Warn<T>(String UserId, String Action, T obj, string message) where T : class
        {
            if (Iloger != null && Iloger.IsWarnEnabled)
            {
                Iloger.Warn(strOriginalString, UserId, Action, GetObjJson(obj, message));
            }
        }
        public void Warn(String UserId, String Action, string message)
        {
            if (Iloger != null && Iloger.IsWarnEnabled)
            {
                Iloger.Warn(strOriginalString, UserId, Action, message);
            }
        }

        public void Error<T>(String UserId, String Action, T obj) where T : class
        {
            Error(UserId, Action, obj, string.Empty);
        }
        public void Error<T>(String UserId, String Action, T obj, string message) where T : class
        {
            if (Iloger != null && Iloger.IsErrorEnabled)
            {
                Iloger.Error(strOriginalString, UserId, Action, GetObjJson(obj, message));
            }
        }
        public void Error(String UserId, String Action, string message)
        {
            if (Iloger != null && Iloger.IsErrorEnabled)
            {
                Iloger.Error(strOriginalString, UserId, Action, message);
            }
        }

        public void Fatal<T>(String UserId, String Action, T obj) where T : class
        {
            Fatal(UserId, Action, obj, string.Empty);
        }
        public void Fatal<T>(String UserId, String Action, T obj, string message) where T : class
        {
            if (Iloger != null && Iloger.IsFatalEnabled)
            {
                Iloger.Fatal(strOriginalString, UserId, Action, GetObjJson(obj, message));
            }
        }
        public void Fatal(String UserId, String Action, string message)
        {
            if (Iloger != null && Iloger.IsFatalEnabled)
            {
                Iloger.Fatal(strOriginalString, UserId, Action, message);
            }
        }

        private string GetObjJson<T>(T obj, String message) where T : class
        {
            if (String.IsNullOrEmpty(message) == false)
            {
                return message;
            }
            else
            {
                return "";
            }
        }
    }

    public class RecordSmtp : RecordLogs
    {
        public RecordSmtp()
        {
            InitIloger();
        }
        public override void InitIloger()
        {
            base.Iloger = SirvaN.ExtLog4Net.MyLogManager.GetLogger("SmtpAppender");
        }
    }
    public class RecordADOErrot : RecordLogs
    {
        public RecordADOErrot()
        {
            InitIloger();
        }
        public override void InitIloger()
        {
            base.Iloger = SirvaN.ExtLog4Net.MyLogManager.GetLogger("AdoNetErrorAppender");
        }
    }
}
