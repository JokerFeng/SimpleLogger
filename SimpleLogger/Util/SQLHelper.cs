using System;
using System.Data.SqlClient;


namespace SimpleLogger.Util
{
    #region SQL数据库帮助类
    /// <summary>
    /// SQL数据库帮助类
    /// </summary>
    class SQLHelper
    {
        #region 根据表名称查询是否存在
        /// <summary>
        /// 根据表名称查询是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        internal static bool CheckTableExist(string tableName)
        {
            using (SqlConnection conn = new SqlConnection(Golbal.SectionLogger.DataBaseSave.ConnectString))
            {
                conn.Open();
                string sql = String.Format("select 1 from sysobjects where name='{0}'", tableName);
                SqlCommand cmd = new SqlCommand(sql, conn);
                object tableExist = cmd.ExecuteScalar();
                if (tableExist == null)
                {
                    return true;
                }
                cmd.Clone();
                conn.Close();
            }
            return false;
        }
        #endregion

        #region 创建数据表
        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="sql"></param>
        internal static void CreateTable(string sql)
        {
            using (SqlConnection conn = new SqlConnection(Golbal.SectionLogger.DataBaseSave.ConnectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                cmd.Clone();
                conn.Close();
            }
        }
        #endregion

        #region 执行一条增删改的SQL语句
        /// <summary>
        /// 执行一条增删改的SQL语句
        /// </summary>
        /// <param name="sql"></param>
        internal static int ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(Golbal.SectionLogger.DataBaseSave.ConnectString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                return cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
    #endregion
}
