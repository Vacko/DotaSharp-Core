using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Interfaces
{
    [StructLayout(LayoutKind.Explicit)]
    internal class ConCommandVTable
    {
        #region Fields

        [FieldOffset(0x04 * 4)] public IntPtr AddFlags4;

        [FieldOffset(0x04 * 6)] public IntPtr GetFlags6;

        [FieldOffset(0x04 * 8)] public IntPtr GetHelpText8;

        [FieldOffset(0x04 * 7)] public IntPtr GetName7;

        [FieldOffset(0x04 * 3)] public IntPtr IsFlagSet3;

        [FieldOffset(0x04 * 5)] public IntPtr RemoveFlags5;

        #endregion
    }

    internal class CConCommand : InteropHelp.NativeWrapper<ConCommandVTable>
    {
        #region Constructors

        public CConCommand(IntPtr conCommand)
        {
            SetupFunctions(conCommand);
        }

        #endregion

        #region Public Methods

        public void AddFlags(ConVarFlags flags) =>
            GetFunction<NativeAddFlags>(Functions.AddFlags4)(ObjectAddress, flags);

        public int GetFlags() => GetFunction<NativeGetFlags>(Functions.GetFlags6)(ObjectAddress);

        public string GetHelpText() =>
            Marshal.PtrToStringAnsi(GetFunction<NativeGetHelpText>(Functions.GetHelpText8)(ObjectAddress));

        public string GetName() =>
            Marshal.PtrToStringAnsi(GetFunction<NativeGetName>(Functions.GetName7)(ObjectAddress));

        public bool IsFlagSet(ConVarFlags flag) =>
            GetFunction<NativeIsFlagSet>(Functions.IsFlagSet3)(ObjectAddress, flag);

        public void RemoveFlags(ConVarFlags flags) =>
            GetFunction<NativeRemoveFlags>(Functions.RemoveFlags5)(ObjectAddress, flags);

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeIsFlagSet(IntPtr thisptr, ConVarFlags flag);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeAddFlags(IntPtr thisptr, ConVarFlags flags);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeRemoveFlags(IntPtr thisptr, ConVarFlags flags);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int NativeGetFlags(IntPtr thisptr);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        private delegate IntPtr NativeGetName(IntPtr thisptr);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        private delegate IntPtr NativeGetHelpText(IntPtr thisptr);

        #endregion
    }
}