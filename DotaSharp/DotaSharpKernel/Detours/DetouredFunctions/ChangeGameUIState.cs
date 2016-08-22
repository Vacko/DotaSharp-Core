using System;
using System.Runtime.InteropServices;
using DotaSharp.Utils;

namespace DotaSharp.DotaSharpKernel.Detours.DetouredFunctions
{
    internal static class ChangeGameUiState
    {
        #region Static Methods

        internal static void CreateHook()
        {
            Game.OnGameUiState += Game.OnChangeGameUiState;
            Game.ChangeGameUiState(new ChangeGameUiStateArgs(UiState.DotaGameUiStateInvalid));

            IntPtr functionUiState = DetoursSignatures.GetChangeGameUiState();

            if (functionUiState != IntPtr.Zero)
            {
                NativeChangeGameUiState originalFunction =
                    (NativeChangeGameUiState) Marshal.GetDelegateForFunctionPointer(functionUiState,
                        typeof(NativeChangeGameUiState));
                NativeChangeGameUiState newFunction = Hooked_ChangeGameUIState;
                DetoursMgr.GetDetours.Create(originalFunction, newFunction, "CGameUI::ChangeGameUIState");

                Logging.Write(false, $"ChangeGameUIState")("ChangeGameUIState() has been Successfull");
            }
            else
            {
                Logging.Write(true, $"ChangeGameUIState")("ChangeGameUIState() not Found");
            }
        }

        private static bool Hooked_ChangeGameUIState(IntPtr @this, int state)
        {
            Game.ChangeGameUiState(new ChangeGameUiStateArgs((UiState) state));

            return (bool) DetoursMgr.GetDetours["CGameUI::ChangeGameUIState"].CallOriginal(@this, state);
        }

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeChangeGameUiState(IntPtr @this, int state);

        #endregion
    }
}