using System;
using System.Runtime.InteropServices;
using DotaSharp.Utils;

namespace DotaSharp.DotaSharpKernel.Detours.DetouredFunctions
{
    internal static class ChangeGameRulesState
    {
        #region Static Methods

        internal static void CreateHook()
        {
            Game.OnGameState += Game.OnChangeGameState;

            Game.ChangeGameState(new ChangeGameStateArgs(GameState.DotaGamerulesStateInit));

            IntPtr functionRulesState = DetoursSignatures.GetChangeGameRulesState();

            if (functionRulesState != IntPtr.Zero)
            {
                NativeChangeGameRulesState originalFunction =
                    (NativeChangeGameRulesState) Marshal.GetDelegateForFunctionPointer(functionRulesState,
                        typeof(NativeChangeGameRulesState));
                NativeChangeGameRulesState newFunction = Hooked_ChangeGameRulesState;
                DetoursMgr.GetDetours.Create(originalFunction, newFunction, "C_DOTAGamerules::State_Enter");

                Logging.Write(false, $"ChangeGameRulesState")("ChangeGameRulesState() has been Successfull");
            }
            else
            {
                Logging.Write(true, $"ChangeGameRulesState")("ChangeGameRulesState() not Found");
            }
        }

        private static IntPtr Hooked_ChangeGameRulesState(IntPtr @this, int state)
        {
            Game.ChangeGameState(new ChangeGameStateArgs((GameState) state));

            return (IntPtr) DetoursMgr.GetDetours["C_DOTAGamerules::State_Enter"].CallOriginal(@this, state);
        }

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeChangeGameRulesState(IntPtr @this, int state);

        #endregion
    }
}