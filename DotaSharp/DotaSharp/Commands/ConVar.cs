using System;
using DotaSharp.DotaSharpKernel.Interfaces;
using DotaSharp.Utils;

namespace DotaSharp
{
    public class ConVar
    {
        #region Fields

        private readonly CConVar m_var;

        #endregion

        #region Constructors

        internal ConVar(IntPtr pointer, string conVarName)
        {
            if (pointer != IntPtr.Zero)
                m_var = new CConVar(pointer);
            else
                Logging.Write(true, $"ConVar")($"'{conVarName}' not Found");
        }

        #endregion

        #region Public Methods

        public void AddFlags(ConVarFlags flags) => m_var?.AddFlags(flags);

        public int GetFlags() => m_var?.GetFlags() ?? 0;

        public string GetHelpText() => m_var != null ? m_var.GetHelpText() : "";

        public string GetName() => m_var != null ? m_var.GetName() : "";

        public bool GetValueBool() => m_var != null && m_var.GetValueBool();

        public int GetValueInt() => m_var?.GetValueInt() ?? 0;

        public bool IsFlagSet(ConVarFlags flag) => m_var != null && m_var.IsFlagSet(flag);

        public void RemoveFlags(ConVarFlags flags) => m_var?.RemoveFlags(flags);

        public void SetValue(string value) => m_var?.SetValue(value);

        public void SetValue(float value) => m_var?.SetValue(value);

        public void SetValue(int value) => m_var?.SetValue(value);

        #endregion
    }
}