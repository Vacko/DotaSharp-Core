using System;
using DotaSharp.DotaSharpKernel.Interfaces;
using DotaSharp.Utils;

namespace DotaSharp
{
    public class ConCommand
    {
        #region Fields

        private readonly CConCommand m_command;

        #endregion

        #region Constructors

        internal ConCommand(IntPtr pointer, string conCommandName)
        {
            if (pointer != IntPtr.Zero)
                m_command = new CConCommand(pointer);
            else
                Logging.Write(true, $"ConCommand")($"'{conCommandName}' not Found");
        }

        #endregion

        #region Public Methods

        public void AddFlags(ConVarFlags flags) => m_command?.AddFlags(flags);

        public int GetFlags() => m_command?.GetFlags() ?? 0;

        public string GetHelpText() => m_command?.GetHelpText() ?? "";

        public string GetName() => m_command?.GetName() ?? "";

        public bool IsFlagSet(ConVarFlags flag) => m_command != null && m_command.IsFlagSet(flag);

        public void RemoveFlags(ConVarFlags flags) => m_command?.RemoveFlags(flags);

        #endregion
    }
}