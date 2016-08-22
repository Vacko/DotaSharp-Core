using System;
using System.Diagnostics;
using DotaSharp.DotaSharpKernel;
using DotaSharp.DotaSharpKernel.Detours;
using DotaSharp.DotaSharpKernel.Misc;
using DotaSharp.Utils;

namespace DotaSharp
{
    internal static class Bootstrap
    {
        #region Fields

        private static bool m_initialized;

        #endregion

        #region Static Methods

        [STAThread]
        private static int EntryPoint(string args)
        {
            if (m_initialized) return 0;

            m_initialized = true;

            Logging.Write(true)("Process (" + Process.GetCurrentProcess().ProcessName + ") [" +
                                Process.GetCurrentProcess().Id + "] Found");
            Logging.LogAllExceptions();

            //Get All Signatures
            Signatures.LoadSignatures();

            //Initialize Hooking System
            DetoursMgr.Initialize();

            //Initialize Loading Assemblies
            Assemblies.Initialize();

            return 0;
        }

        #endregion
    }
}