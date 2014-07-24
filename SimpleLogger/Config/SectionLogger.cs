using System;
using System.Configuration;


namespace SimpleLogger.Config
{
    public class SectionLogger : ConfigurationSection
    {
        #region 本地存储配置节点
        /// <summary>
        /// 本地存储配置节点
        /// </summary>
        [ConfigurationProperty("LocalSave")]
        public LocalSaveElement LocalSave
        {
            get { return (LocalSaveElement)this["LocalSave"]; }
        }
        #endregion

        #region 数据库存储配置节点
        /// <summary>
        /// 数据库存储配置节点
        /// </summary>
        [ConfigurationProperty("DataBaseSave")]
        public DataBaseSaveElement DataBaseSave
        {
            get { return (DataBaseSaveElement)this["DataBaseSave"]; }
        }
        #endregion

        #region  是否启用所有的功能（日记保存到本地和数据库），Bool类型
        /// <summary>
        /// 是否启用所有的功能（日记保存到本地和数据库），Bool类型
        /// </summary>
        [ConfigurationProperty("Open", IsRequired=true)]
        public bool Open
        {
            get { return bool.Parse(this["Open"].ToString()); }
            set { this["Open"] = value; }
        }
        #endregion
    }

    public class LocalSaveElement : ConfigurationElement
    {
        #region 是否启用日记保存到本地文件，Bool类型
        /// <summary>
        /// 是否启用日记保存到本地文件，Bool类型
        /// </summary>
        [ConfigurationProperty("Open")]
        public bool Open
        {
            get { return bool.Parse(this["Open"].ToString()); }
            set { this["Open"] = value; }
        }
        #endregion

        #region 保存的本地路径
        /// <summary>
        /// 保存的本地路径
        /// </summary>
        [ConfigurationProperty("FilePath")]
        public string FilePath
        {
            get { return this["FilePath"].ToString(); }
        }
        #endregion

        #region 保存文件的扩展名
        /// <summary>
        /// 保存文件的扩展名
        /// </summary>
        [ConfigurationProperty("FileExtension")]
        public string FileExtension
        {
            get { return this["FileExtension"].ToString(); }
        }
        #endregion

    }

    public class DataBaseSaveElement : ConfigurationElement
    {

        #region 是否启用日记保存到数据库，Bool类型
        /// <summary>
        /// 是否启用日记保存到数据库，Bool类型
        /// </summary>
        [ConfigurationProperty("Open")]
        public bool Open
        {
            get { return bool.Parse(this["Open"].ToString()); }
            set { this["Open"] = value; }
        }
        #endregion

        #region 数据库连接字符串
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        [ConfigurationProperty("ConnectString", IsRequired = true)]
        public string ConnectString
        {
            get { return this["ConnectString"].ToString(); }
            set { this["ConnectString"] = value; }
        }
        #endregion

        #region 数据表名称，没有则自动创建
        /// <summary>
        /// 数据表名称，没有则自动创建
        /// </summary>
        [ConfigurationProperty("TableName")]
        public string TableName
        {
            get { return this["TableName"].ToString(); }
            set { this["TableName"] = value; }
        }
        #endregion

    }
}
