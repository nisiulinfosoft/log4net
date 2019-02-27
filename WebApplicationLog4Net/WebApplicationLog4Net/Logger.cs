using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplicationLog4Net
{

    //https://www.youtube.com/watch?v=WUbprKbJXgs
    //https://jonmunoa.blog/2017/01/09/log4net-libreria-para-crear-logs-en-net/
    //https://geeks.ms/eortuno/2013/02/22/usando-log4net-una-manera-rapida/
    //https://github.com/kaizenforce/Log4Net/blob/master/Web.config

    public class Logger
    {
        #region Fields

        private readonly ILog mLog = null; //LogManager.GetLogger(typeof(Logger));

        //private static readonly ILog mLog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Niveles de log y mensajes
        //FATAL
        //ERROR
        //WARN
        //INFO
        //DEBUG

        //Niveles de logs:
        //OFF(no se loguea)
        //FATAL
        //ERROR
        //WARN
        //INFO
        //DEBUG
        //ALL(logueamos todo)

        #endregion Fields

        #region Constructor

        public Logger()
        {
        }

        public Logger(ILog mLog)
        {
            this.mLog = mLog;

            iniciarConfiguracion();
        }

        #endregion Constructor

        #region Public Methods

        public void iniciarConfiguracion()
        {
            Debug.WriteLine("Server Logger initializing...");

            //Solo se debe iniciar una sola vez
            log4net.Config.XmlConfigurator.Configure();

            if (this.mLog != null)
            {
                Debug.WriteLine("Server Logger initialized");
                Debug.WriteLine(string.Format("Debug: {0}, Error: {1}, Info: {2}, Warning {3}, Fatal {4}", this.mLog.IsDebugEnabled, this.mLog.IsErrorEnabled, this.mLog.IsInfoEnabled, this.mLog.IsWarnEnabled, this.mLog.IsFatalEnabled));
            }
            else
            {
                Debug.WriteLine("Failed initializing Server Logger");
            }
        }

        public void PublishException(Exception exception)
        {
            if (this.mLog != null)
                this.mLog.Error("Exception", exception);
        }

        public void WriteVerbose(string category, string message)
        {
            if (this.mLog != null)
                this.mLog.Debug(FormatMessage(category, message));
        }

        public void WriteInfo(string category, string message)
        {
            if (this.mLog != null)
                this.mLog.Info(FormatMessage(category, message));
        }

        public void WriteWarning(string category, string message)
        {
            if (this.mLog != null)
                this.mLog.Warn(FormatMessage(category, message));
        }

        public void WriteFatal(string category, string message)
        {
            if (this.mLog != null)
                this.mLog.Fatal(FormatMessage(category, message));
        }

        public void TraceError(string category, string message)
        {
            if (this.mLog != null)
                this.mLog.Error(FormatMessage(category, message));
        }

        public void Write(TraceLevel level, string category, string message)
        {
            switch (level)
            {
                case TraceLevel.Verbose:
                    WriteVerbose(category, message);
                    break;
                case TraceLevel.Info:
                    WriteInfo(category, message);
                    break;
                case TraceLevel.Warning:
                    WriteWarning(category, message);
                    break;
                case TraceLevel.Error:
                    TraceError(category, message);
                    break;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private const string MessageFormat = "Type: {0} | Msg: {1}";
        private const int MaxCategoryNameLength = 15;

        public string FormatMessage(string category, string message)
        {
            string output = string.Format(MessageFormat, FormatName(category,MaxCategoryNameLength), message);
            return output;
        }

        public string FormatName(string name, int minLength)
        {
            string result;
            string trimName = name != null ? name.Trim() : string.Empty;
            if (trimName.Length >= minLength)
                result = trimName;
            else
                result = trimName.PadRight(minLength);
            return result;
        }

        #endregion Private Methods

    }
}