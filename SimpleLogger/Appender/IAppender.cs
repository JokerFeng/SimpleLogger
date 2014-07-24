using System;

namespace SimpleLogger.Appender
{
    interface IAppender
    {
        /// <summary>
        ///记录日记
        /// </summary>
        /// <param name="loggerContext">错误信息模型</param>
        void Append<T>(T model);
    }
}
