using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Interfaces
{
    [StructLayout(LayoutKind.Explicit)]
    internal class VPanelWrapperVTable
    {
        #region Fields

        public const int PaintTraverse = 55;

        [FieldOffset(0x04 * 50)] public IntPtr GetName;

        #endregion
    }

    [InterfaceVersions("VGUI_Panel010")]
    internal class VPanelWrapper : InteropHelp.NativeWrapper<VPanelWrapperVTable>
    {
        #region Fields

        private static VPanelWrapper m_instance;
        private static readonly object Padlock = new object();

        #endregion

        #region Properties

        public static VPanelWrapper Instance
        {
            get
            {
                lock (Padlock)
                {
                    return m_instance ?? (m_instance = Dota.CreateInterface<VPanelWrapper>("vgui2.dll"));
                }
            }
        }

        #endregion

        #region Public Methods

        public string GetPanelName(ulong panel) =>
            Marshal.PtrToStringAnsi(GetFunction<NativeGetPanelName>(Functions.GetName)(ObjectAddress, panel));

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        private delegate IntPtr NativeGetPanelName(IntPtr @this, ulong panel);

        #endregion
    }
}