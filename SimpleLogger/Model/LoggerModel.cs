using System;

namespace SimpleLogger.Model
{
    #region 错误等级
    /// <summary>
    /// 错误等级
    /// </summary>
    public enum ErrorLevel
    {
        /// <summary>
        /// 轻微级
        /// </summary>
        Mild,
        /// <summary>
        /// 一般级
        /// </summary>
        General,
        /// <summary>
        /// 严重级
        /// </summary>
        Serious,
        /// <summary>
        /// 灾难级
        /// </summary>
        Deadly
    }
    #endregion

    #region 错误信息模型表
    /// <summary>
    /// 错误信息模型表
    /// </summary>
    public class LoggerModel
    {
        /// <summary>
        /// 异常发生时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 获取描述当前异常的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        ///获取或设置导致错误的应用程序或对象的名称
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        ///  获取调用堆栈上直接帧的字符串表示形式
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }
        /// <summary>
        /// 错误等级
        /// </summary>
        public ErrorLevel ErrorLevel { get; set; }
    }
    #endregion
}
