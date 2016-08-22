using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Interfaces
{
    [StructLayout(LayoutKind.Explicit)]
    internal class CCvarVTable
    {
        #region Fields

        [FieldOffset(0x04 * 20)] public IntPtr FindCommand;

        [FieldOffset(0x04 * 18)] public IntPtr FindVar;

        #endregion
    }

    [InterfaceVersions("VEngineCvar007")]
    internal class CCvar : InteropHelp.NativeWrapper<CCvarVTable>
    {
        #region Fields

        private static CCvar m_instance;
        private static readonly object Padlock = new object();

        #endregion

        #region Properties

        public static CCvar Instance
        {
            get
            {
                lock (Padlock)
                {
                    return m_instance ?? (m_instance = Dota.CreateInterface<CCvar>("vstdlib.dll"));
                }
            }
        }

        #endregion

        #region Public Methods

        public unsafe IntPtr FindCommand(sbyte* command) =>
            GetFunction<NativeFindCommand>(Functions.FindCommand)(ObjectAddress, command);

        public unsafe IntPtr FindVar(sbyte* command) =>
            GetFunction<NativeFindVar>(Functions.FindVar)(ObjectAddress, command);

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private unsafe delegate IntPtr NativeFindVar(IntPtr thisptr, sbyte* command);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private unsafe delegate IntPtr NativeFindCommand(IntPtr thisptr, sbyte* command);

        #endregion
    }
}