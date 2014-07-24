using SimpleLogger.Util;
using System;
using System.Reflection;
using System.Text;

namespace SimpleLogger.Appender
{
    class DataBaseSaveAppend : IAppender
    {

        #region 表格名称
        /// <summary>
        /// 表格名称
        /// </summary>
        public static string TableName
        {
            get
            {
                return String.IsNullOrEmpty(Golbal.SectionLogger.DataBaseSave.TableName) ? "SimpleLogger" : Golbal.SectionLogger.DataBaseSave.TableName;
            }
        }
        #endregion

        public void Append<T>(T model)
        {
            if (SQLHelper.CheckTableExist(TableName))
            {
                SQLHelper.CreateTable(CreateTableSQL(model));
            }
            int res = SQLHelper.ExecuteNonQuery(InsertTableSQL(model));
        }

        #region 生成创建表格SQL语句
        /// <summary>
        /// 生成创建表格SQL语句
        /// </summary>
        /// <returns></returns>
        private static string CreateTableSQL<T>(T model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("CREATE TABLE {0} (", TableName);
            Type loggerContextType = model.GetType();
            PropertyInfo[] propertyInfos = loggerContextType.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (!propertyInfo.PropertyType.IsEnum)
                {
                    sqlBuilder.AppendFormat(" {0} {1},", propertyInfo.Name, SQLGetType(propertyInfo.PropertyType.FullName));
                }
                else
                {
                    sqlBuilder.AppendFormat(" {0} {1},", propertyInfo.Name, SQLGetType(propertyInfo.PropertyType.FullName, true));
                }
            }
            sqlBuilder.Remove(sqlBuilder.Length - 1, 1).Append(")");
            return sqlBuilder.ToString();
        }
        #endregion

        #region 插入数据的SQL语句
        /// <summary>
        /// 插入数据的SQL语句
        /// </summary>
        /// <returns></returns>
        private static string InsertTableSQL<T>(T model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("insert into  {0} (", TableName);
            Type loggerContextType = model.GetType();
            PropertyInfo[] propertyInfos = loggerContextType.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                sqlBuilder.AppendFormat("{0},", propertyInfo.Name);
            }
            sqlBuilder.Remove(sqlBuilder.Length - 1, 1).Append(") ").Append(" Values(");
            foreach (var propertyInfo in propertyInfos)
            {
                sqlBuilder.AppendFormat("'{0}',", propertyInfo.GetValue(model, null));
            }
            sqlBuilder.Remove(sqlBuilder.Length - 1, 1).Append(")");
            return sqlBuilder.ToString();
        }
        #endregion

        #region C#数据类型转换成SQL数据库类型
        /// <summary>
        /// C#数据类型转换成SQL数据库类型
        ///  详情查看：http://msdn.microsoft.com/zh-cn/library/cc716729.aspx
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isEnum"></param>
        /// <returns></returns>
        private static string SQLGetType(string type, bool isEnum = false)
        {

            if (isEnum)
            {
                return "nvarchar(255)";
            }
            switch (type.ToString())
            {
                case "System.Int64": return "bigint";
                case "System.Byte[]": return "binary";
                case "System.Boolean": return "bit";
                case "System.Char": return "char";
                case "System.DateTime": return "datetime";
                case "System.Decimal": return "decimal";
                case "System.Double": return "float";
                case "System.Int32": return "int";
                case "System.String": return "nvarchar(MAX)";
                case "System.Single": return "real";
                case "System.Int16": return "smallint";
                case "System.Guid": return "uniqueidentifier";
                default:
                    throw new Exception(type.ToString() + " not implemented.");
            }
        }
        #endregion


    }
}
