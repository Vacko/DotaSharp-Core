using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Interfaces
{
    [StructLayout(LayoutKind.Explicit)]
    internal class CSource2ClientVTable
    {
        #region Fields

        public const int CreateMove = 15;

        [FieldOffset(0x04 * 26)] public IntPtr GetAllClasses26;

        [FieldOffset(0x04 * 23)] public IntPtr GetEntity2ClassName23;

        [FieldOffset(0x04 * 86)] public IntPtr GetEntityInfo;

        #endregion
    }

    [InterfaceVersions("Source2Client002")]
    internal class CSource2Client : InteropHelp.NativeWrapper<CSource2ClientVTable>
    {
        #region Fields

        private static CSource2Client m_instance;
        private static readonly object Padlock = new object();

        #endregion

        #region Properties

        public static CSource2Client Instance
        {
            get
            {
                lock (Padlock)
                {
                    return m_instance ?? (m_instance = Dota.CreateInterface<CSource2Client>("client.dll"));
                }
            }
        }

        #endregion

        #region Public Methods

        public IntPtr GetEntityInfo() => GetFunction<NativeGetEntityInfo>(Functions.GetEntityInfo)(ObjectAddress);

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetEntityInfo(IntPtr thisptr);

        #endregion
    }
}