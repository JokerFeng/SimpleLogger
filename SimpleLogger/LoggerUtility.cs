using SimpleLogger.Appender;
using SimpleLogger.Model;
using SimpleLogger.Util;
using System;


namespace SimpleLogger
{
    public class LoggerUtility
    {
        /// <summary>
        /// 记录日记
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLog(Exception ex)
        {
            WriteLog<LoggerModel>(ex, new LoggerModel());
        }

        /// <summary>
        /// 记录日记
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="model"></param>
        public static void WriteLog<T>(Exception ex, T model) where T : LoggerModel
        {
            OpenType type;
            if (SectionHelper.IsOpen(out type))
            {
                T context = Initializate<T>(ex, model);

                if (type.HasFlag(OpenType.LocalSave))
                {
                    IAppender append = new LocalSaveAppend();
                    append.Append<T>(model);
                }
                if (type.HasFlag(OpenType.DataBaseSave))
                {
                    IAppender append = new DataBaseSaveAppend();
                    append.Append(model);
                }
            }
        }

        #region 初始化数据储蓄数据Model
        /// <summary>
        /// 初始化数据储蓄数据Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private static T Initializate<T>(Exception ex, T model) where T : LoggerModel
        {
            LoggerModel loggerContext = model as LoggerModel;
            loggerContext.Time = DateTime.Now;
            loggerContext.ErrorLevel = ErrorLevel.Mild;
            loggerContext.ExceptionType = ex.GetType().ToString();
            loggerContext.Message = ex.Message;
            loggerContext.Source = ex.Source;
            loggerContext.StackTrace = ex.StackTrace;
            return loggerContext as T;
        }
        #endregion

    }
}
