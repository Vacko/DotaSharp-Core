using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Interfaces
{
    [StructLayout(LayoutKind.Explicit)]
    internal class CEngineClientVTable
    {
        #region Fields

        [FieldOffset(0x04 * 38)] public IntPtr ClientCmd_Unrestricted;

        [FieldOffset(0x04 * 36)] public IntPtr ExecuteClientCmd;

        [FieldOffset(0x04 * 22)] public IntPtr GetLocalPlayer;

        [FieldOffset(0x04 * 32)] public IntPtr GetNetChannelInfo;

        [FieldOffset(0x04 * 20)] public IntPtr GetPlayerInfo;

        [FieldOffset(0x04 * 30)] public IntPtr IsConnected;

        [FieldOffset(0x04 * 49)] public IntPtr IsDrawingLoadingImage;

        [FieldOffset(0x04 * 29)] public IntPtr IsInGame;

        #endregion
    }

    [InterfaceVersions("Source2EngineToClient001")]
    internal class CEngineClient : InteropHelp.NativeWrapper<CEngineClientVTable>
    {
        #region Fields

        private static CEngineClient m_instance;
        private static readonly object Padlock = new object();

        #endregion

        #region Properties

        public static CEngineClient Instance
        {
            get
            {
                lock (Padlock)
                {
                    return m_instance ?? (m_instance = Dota.CreateInterface<CEngineClient>("engine2.dll"));
                }
            }
        }

        #endregion

        #region Public Methods

        public unsafe void ClientCmd_Unrestricted(sbyte* command) =>
            GetFunction<NativeClientCmdUnrestricted>(Functions.ClientCmd_Unrestricted)(ObjectAddress, command);

        public unsafe void ExecuteClientCmd(sbyte* command) =>
            GetFunction<NativeExecuteClientCmd>(Functions.ExecuteClientCmd)(ObjectAddress, command);

        public IntPtr GetLocalPlayer(int index) =>
            GetFunction<NativeGetLocalPlayer>(Functions.GetLocalPlayer)(ObjectAddress, ref index, 0);

        public IntPtr GetNetChannelInfo() =>
            GetFunction<NativeGetNetChannelInfo>(Functions.GetNetChannelInfo)(ObjectAddress, 0);

        public bool IsConnected() => GetFunction<NativeIsConnected>(Functions.IsConnected)(ObjectAddress).Equals(1);

        public bool IsDrawingLoadingImage() =>
            GetFunction<NativeIsDrawingLoadingImage>(Functions.IsDrawingLoadingImage)(ObjectAddress).Equals(1);

        public bool IsInGame() => GetFunction<NativeIsInGame>(Functions.IsInGame)(ObjectAddress).Equals(1);

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetLocalPlayer(IntPtr thisptr, ref int index, int cSplitScreenSlot);

        [return: MarshalAs(UnmanagedType.I1)]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int NativeIsInGame(IntPtr thisptr);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int NativeIsConnected(IntPtr thisptr);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private unsafe delegate void NativeExecuteClientCmd(IntPtr thisptr, sbyte* command);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private unsafe delegate void NativeClientCmdUnrestricted(IntPtr thisptr, sbyte* command);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetNetChannelInfo(IntPtr thisptr, int cSplitScreenSlot);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int NativeIsDrawingLoadingImage(IntPtr thisptr);

        #endregion
    }
}