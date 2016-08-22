using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Interfaces
{
    [StructLayout(LayoutKind.Explicit)]
    internal class CGameEventManagerVTable
    {
        #region Fields

        [FieldOffset(0x04 * 5)] public IntPtr AddListener;

        #endregion
    }

    internal class CGameEventManager : InteropHelp.NativeWrapper<CGameEventManagerVTable>
    {
        #region Constructors

        public CGameEventManager(IntPtr cGameEventManager)
        {
            SetupFunctions(cGameEventManager);
        }

        #endregion

        #region Public Methods

        public unsafe bool AddListener(IntPtr gameEventListner2, sbyte* Event, bool bServerSide) =>
            GetFunction<NativeAddListener>(Functions.AddListener)(ObjectAddress, gameEventListner2, Event, bServerSide);

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private unsafe delegate bool NativeAddListener(IntPtr @this, IntPtr gameEventListner2, sbyte* Event,
            bool bServerSide);

        #endregion
    }
}