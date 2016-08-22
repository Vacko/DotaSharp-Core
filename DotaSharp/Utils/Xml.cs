using System.IO;
using System.Xml;

namespace DotaSharp.Utils
{
    internal static class Xml
    {
        #region Fields

        private const string XmlFile = "Settings.xml";
        private const string XmlRoot = "Assemblies";
        private static readonly string XmlPath = ApplicationInfo.PathO + @"\" + XmlFile;

        #endregion

        #region Static Methods

        internal static string ReadElement(string element)
        {
            if (!File.Exists(XmlPath)) return string.Empty;

            XmlReader lReader = XmlReader.Create(XmlPath);

            lReader.Read();
            lReader.ReadStartElement(XmlRoot);

            while (lReader.Read())
                if (lReader.Name == element)
                {
                    string r = lReader.ReadString();
                    lReader.Close();
                    return r;
                }

            lReader.Close();

            return string.Empty;
        }

        #endregion
    }
}