using System;
using System.IO;
using System.Linq;
using System.Reflection;
using DotaSharp.Utils;

namespace DotaSharp.DotaSharpKernel
{
    internal static class Assemblies
    {
        #region Fields

        private static bool m_initialized;

        #endregion

        #region Static Methods

        internal static void Initialize()
        {
            if (m_initialized) return;

            m_initialized = true;

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += currentDomain_AssemblyResolve;


            if (Directory.Exists(ApplicationInfo.PathO + @"\Plugins"))
                foreach (string assemblyDll in Directory.GetFiles(ApplicationInfo.PathO + @"\Plugins", "*.dll"))
                    if (Xml.ReadElement(Path.GetFileNameWithoutExtension(assemblyDll)) == "true")
                        LoadAssembly(assemblyDll);
                    else
                        Directory.CreateDirectory(ApplicationInfo.PathO + @"\Plugins");
        }

        private static void LoadAssembly(string assemblyPath)
        {
            Assembly a = Assembly.LoadFile(assemblyPath);

            Logging.Write(true)("Loading Assembly '" + Path.GetFileNameWithoutExtension(assemblyPath) + "' Version=" +
                                a.GetName().Version);

            Type myType = a.GetTypes().SingleOrDefault(t => t.Name == "Program");

            MethodInfo myMethod = myType.GetMethod("Main");

            object obj = Activator.CreateInstance(myType);

            try
            {
                myMethod.Invoke(obj, null);
            }
            catch (Exception ex)
            {
                Logging.Write(true)("{0}", ex.ToString());
            }
        }

        private static Assembly currentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string strTempAssmbPath = "";

            if (args.Name.Substring(0, args.Name.IndexOf(",", StringComparison.Ordinal)) == $"DotaSharp")
                strTempAssmbPath = ApplicationInfo.PathA;

            Assembly myAssembly = Assembly.LoadFrom(strTempAssmbPath);

            return myAssembly;
        }

        #endregion
    }
}