using System;
using System.IO;
using System.Reflection;

namespace DotaSharp.Utils
{
    public static class ApplicationInfo
    {
        #region Fields

        /// <summary>
        ///     Application
        /// </summary>
        public static readonly string PathA = Assembly.GetExecutingAssembly().GetFiles()[0].Name;

        /// <summary>
        ///     Application Path
        /// </summary>
        public static readonly string PathO = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetFiles()[0].Name);

        /// <summary>
        ///     Injected Application Path
        /// </summary>
        public static readonly string PathI = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        ///     Log File Name
        /// </summary>
        public static readonly string LogFileName = DateTime.Now.ToString("d").Replace('/', '-') + ".log";

        #endregion
    }
}