using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Interfaces
{
    [StructLayout(LayoutKind.Explicit)]
    internal class CConVarVTable
    {
        #region Fields

        [FieldOffset(0x04 * 4)] public IntPtr AddFlags4; //void AddFlags(int flags);

        [FieldOffset(0x04 * 6)] public IntPtr GetFlags6; //int GetFlags(void);

        [FieldOffset(0x04 * 8)] public IntPtr GetHelpText8; //char* GetHelpText(void);    

        [FieldOffset(0x04 * 7)] public IntPtr GetName7; //char* GetName(void);

        [FieldOffset(0x04 * 15)] public IntPtr GetValueBool15; //bool  GetValueBool(void);

        [FieldOffset(0x04 * 14)] public IntPtr GetValueInt14; //int  GetValueInt(void);

        [FieldOffset(0x04 * 3)] public IntPtr IsFlagSet3; //bool IsFlagSet(int flag);

        [FieldOffset(0x04 * 5)] public IntPtr RemoveFlags5; //void RemoveFlags(int flag);

        [FieldOffset(0x04 * 16)] public IntPtr SetValue16; //void  InternalSetValue(char const* value)

        [FieldOffset(0x04 * 17)] public IntPtr SetValue17; //void  InternalSetValue(float value)

        [FieldOffset(0x04 * 18)] public IntPtr SetValue18; //void  InternalSetValue(int value)

        #endregion
    }

    internal class CConVar : InteropHelp.NativeWrapper<CConVarVTable>
    {
        #region Constructors

        public CConVar(IntPtr cCvar)
        {
            SetupFunctions(cCvar);
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

        public bool GetValueBool() => GetFunction<NativeGetValueBool>(Functions.GetValueBool15)(ObjectAddress);
        public int GetValueInt() => GetFunction<NativeGetValueInt>(Functions.GetValueInt14)(ObjectAddress);

        public bool IsFlagSet(ConVarFlags flag) =>
            GetFunction<NativeIsFlagSet>(Functions.IsFlagSet3)(ObjectAddress, flag);

        public void RemoveFlags(ConVarFlags flags) =>
            GetFunction<NativeRemoveFlags>(Functions.RemoveFlags5)(ObjectAddress, flags);

        public void SetValue(string value) => GetFunction<NativeSetValue16>(Functions.SetValue16)(ObjectAddress, value);
        public void SetValue(float value) => GetFunction<NativeSetValue17>(Functions.SetValue17)(ObjectAddress, value);
        public void SetValue(int value) => GetFunction<NativeSetValue18>(Functions.SetValue18)(ObjectAddress, value);

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

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int NativeGetValueInt(IntPtr thisptr);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool NativeGetValueBool(IntPtr thisptr);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeSetValue16(IntPtr thisptr, string value);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeSetValue17(IntPtr thisptr, float value);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeSetValue18(IntPtr thisptr, int value);

        #endregion
    }
}