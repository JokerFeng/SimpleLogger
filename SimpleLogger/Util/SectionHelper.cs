using SimpleLogger.Config;
using SimpleLogger.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SimpleLogger.Util
{
    [Flags]
    enum OpenType : int
    {
        None = 0,
        LocalSave = 1,
        DataBaseSave = 2,
    }

    class SectionHelper
    {
        static SectionHelper() 
        {
           Golbal.SectionLogger = ConfigurationManager.GetSection("SimpleLogger") as SectionLogger;
        }

        #region 判断开启的记录日记方式
        /// <summary>
        /// 判断开启的记录日记方式
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsOpen(out OpenType type)
        {
            int open = 0;
            if (Golbal.SectionLogger != null && Golbal.SectionLogger.Open)
            {
                if (Golbal.SectionLogger.LocalSave.Open)
                {
                    open += (int)OpenType.LocalSave;
                }
                if (Golbal.SectionLogger.DataBaseSave.Open)
                {
                    open += (int)OpenType.DataBaseSave;
                }
            }
            type = (OpenType)open;
            return open == 0 ? false : true;
        }
        #endregion
    }
}
