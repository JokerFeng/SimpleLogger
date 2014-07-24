using SimpleLogger.Util;
using System;
using System.IO;
using System.Reflection;


namespace SimpleLogger.Appender
{
    class LocalSaveAppend : IAppender
    {
        #region 把错误日记写到本地文件
        /// <summary>
        /// 把错误日记写到本地文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void Append<T>(T model)
        {
            lock (this)
            {
                string path = string.IsNullOrEmpty(Golbal.SectionLogger.LocalSave.FilePath) ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs") : Golbal.SectionLogger.LocalSave.FilePath;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileExtension = string.IsNullOrEmpty(Golbal.SectionLogger.LocalSave.FileExtension) ? ".log" : Golbal.SectionLogger.LocalSave.FileExtension;
                var logFileName = Path.Combine(path, DateTime.Now.ToString("yyyyMMdd") + fileExtension);

                Type modelType = model.GetType();
                PropertyInfo[] propertyInfos = modelType.GetProperties();
                using (StreamWriter sw = new StreamWriter(logFileName, true))
                {
                    sw.WriteLine("=====================================");
                    foreach (var propertyInfo in propertyInfos)
                    {
                        sw.WriteLine("{0}:{1}", propertyInfo.Name, propertyInfo.GetValue(model, null));
                    }
                    sw.WriteLine("=====================================");
                    sw.Dispose();
                    sw.Close();
                }
            }
        }
        #endregion
    }
}
