using System;
using System.Runtime.InteropServices;
using DotaSharp.DotaSharpKernel.Interfaces;
using DotaSharp.Memory;

namespace DotaSharp
{
    public static class Console
    {
        #region Static Methods

        public static unsafe void PrintConsoleMessage(string message)
        {
            IntPtr ptrMsg = WinImports.GetProcAddress(WinImports.GetModuleHandle("Tier0.dll"), "Msg");

            FMsg messageFunction = (FMsg) Marshal.GetDelegateForFunctionPointer(ptrMsg, typeof(FMsg));

            sbyte* msg = MemoryManager.StringToChar(message + "\n");

            messageFunction(msg);
        }

        public static unsafe void PrintConsoleWarning(string message)
        {
            IntPtr ptrWarning = WinImports.GetProcAddress(WinImports.GetModuleHandle("Tier0.dll"), "Warning");

            FMsg warningFunction = (FMsg) Marshal.GetDelegateForFunctionPointer(ptrWarning, typeof(FMsg));

            sbyte* msg = MemoryManager.StringToChar(message + "\n");

            warningFunction(msg);
        }

        public static void PrintChatMessage(string message)
        {
            if (Game.IsInGame)
                ExecuteComman_Unrestricted("say " + message);
        }

        [Obsolete("Use ExecuteComman_Unrestricted", true)]
        public static unsafe void ExecuteCommand(string command)
        {
            sbyte* execute = MemoryManager.StringToChar(command);

            CEngineClient.Instance.ExecuteClientCmd(execute);
        }

        public static unsafe void ExecuteComman_Unrestricted(string command)
        {
            sbyte* execute = MemoryManager.StringToChar(command);

            CEngineClient.Instance.ClientCmd_Unrestricted(execute);
        }

        public static unsafe ConVar FindVar(string conVarName)
        {
            sbyte* execute = MemoryManager.StringToChar(conVarName);

            IntPtr pointerToVar = CCvar.Instance.FindVar(execute);

            return new ConVar(pointerToVar, conVarName);
        }

        public static unsafe ConCommand FindCommand(string command)
        {
            sbyte* execute = MemoryManager.StringToChar(command);

            IntPtr pointerToCommand = CCvar.Instance.FindCommand(execute);

            return new ConCommand(pointerToCommand, command);
        }

        #endregion

        #region Nested Types, Enums, Delegates

        private unsafe delegate void FMsg(sbyte* message);

        #endregion
    }
}