using System;
using DotaSharp.DotaSharpKernel.Interfaces;
using DotaSharp.Memory;

namespace DotaSharp.DotaSharpKernel.Misc
{
    internal static class Signatures
    {
        #region Fields

        internal static IntPtr SignatureGetLocalPlayer = default(IntPtr);
        internal static IntPtr SignatureGetNextEnt = default(IntPtr);
        internal static IntPtr SignatureGetEntityByIndex = default(IntPtr);
        internal static IntPtr SignatureGetPrepareUnitOrders = default(IntPtr);
        internal static IntPtr SignatureGetEntityIndex = default(IntPtr);
        internal static IntPtr SignatureWorldToScreenX = default(IntPtr);
        internal static IntPtr SignatureWorldToScreenY = default(IntPtr);
        internal static IntPtr CSource2GetEntitySystem = default(IntPtr);

        #endregion

        #region Static Methods

        internal static void LoadSignatures()
        {
            SignatureGetLocalPlayer = GetLocalPlayer();
            SignatureGetNextEnt = GetNextEnt();
            SignatureGetEntityByIndex = GetEntityByIndex();
            SignatureGetPrepareUnitOrders = GetPrepareUnitOrder();
            SignatureGetEntityIndex = GetEntityIndex();
            SignatureWorldToScreenX = WorldToScreenX();
            SignatureWorldToScreenY = WorldToScreenY();
            CSource2GetEntitySystem = CSource2Client.Instance.GetEntityInfo();
        }

        internal static IntPtr WorldToScreenY() => MemoryManager.FindSignature(
            new byte[]
            {
                0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x18, 0xF3, 0x0F, 0x10, 0x55, 0x08, 0x8D, 0x45, 0xF4, 0xF3, 0x0F, 0x10,
                0x4D, 0x0C, 0x8D, 0x55, 0x08
            },
            "xxxxxxxxxxxxxxxxxxxxxx",
            "client.dll",
            0xf52000);

        private static IntPtr GetLocalPlayer() => MemoryManager.FindSignature(
            new byte[]
            {
                0x56, 0x8B, 0x35, 0x00, 0x00, 0x00, 0x00, 0x85, 0xF6, 0x74, 0x65, 0x8B, 0x06, 0x8B, 0xCE, 0x8B, 0x80,
                0x00, 0x00, 0x00, 0x00, 0xFF, 0xD0, 0x84, 0xC0, 0x74, 0x55, 0x57, 0xE8, 0x00, 0x00, 0x00, 0x00
            },
            "xxx????xxxxxxxxxx????xxxxxxxx????",
            "client.dll",
            0xf52000);

        private static IntPtr GetNextEnt() => MemoryManager.FindSignature(
            new byte[] {0x55, 0x8B, 0xEC, 0x51, 0x8B, 0xC1, 0x8B, 0x4D, 0x08},
            "xxxxxxxxx",
            "client.dll",
            0xef9000);

        private static IntPtr GetEntityByIndex() => MemoryManager.FindSignature(
            new byte[] {0x55, 0x8B, 0xEC, 0x56, 0x8B, 0x75, 0x08, 0x83, 0xFE, 0xFF},
            "xxxxxxxxxx",
            "client.dll",
            0xf52000);

        private static IntPtr GetPrepareUnitOrder() => MemoryManager.FindSignature(
            new byte[] {0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x7C, 0x80, 0x7D, 0x2C, 0x00, 0x53, 0x56},
            "xxxxxxxxxxxx",
            "client.dll",
            0xf52000);

        private static IntPtr GetEntityIndex() => MemoryManager.FindSignature(
            new byte[] {0x55, 0x8B, 0xEC, 0xFF, 0x71, 0x04, 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00},
            "xxxxxxxx????",
            "client.dll",
            0xf52000);

        private static IntPtr WorldToScreenX() => MemoryManager.FindSignature(
            new byte[]
            {
                0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x18, 0xF3, 0x0F, 0x10, 0x55, 0x08, 0x8D, 0x45, 0xF4, 0xF3, 0x0F, 0x10,
                0x4D, 0x0C, 0x8D, 0x55, 0x0C
            },
            "xxxxxxxxxxxxxxxxxxxxxx",
            "client.dll",
            0xf52000);

        #endregion
    }
}