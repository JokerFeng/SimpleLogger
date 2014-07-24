using SimpleLogger;
using System;


namespace SimpleLoggerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Exception ex = new Exception("111");

            //两种方式只能选择一种，如果切换，请删除数据库中的旧表或者更改webconfig的TableName字段创建新表格

            //(1)方式一，不使用自定义的日记模型
           // LoggerUtility.WriteLog(ex);


            //(2)方式二，使用自定义的日记模型
            LoggerModelExtension model = new LoggerModelExtension()
            {
              //这些属性都不需要手动给值，系统会自动给他们赋这些值给他们，如果手动赋值了，将会被替换掉。
            //Time = DateTime.Now,
            //ErrorLevel = SimpleLogger.Model.ErrorLevel.Mild,
            //ExceptionType = ex.GetType().ToString(),
            //Message = ex.Message,
            //Source = ex.Source,
            //StackTrace = ex.StackTrace,
            Note="自己加的属性"
            };

            LoggerUtility.WriteLog<LoggerModelExtension>(ex, model);

            Console.Read();

        }
    }
}
