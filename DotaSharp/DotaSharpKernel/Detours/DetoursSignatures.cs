using System;
using DotaSharp.Memory;

namespace DotaSharp.DotaSharpKernel.Detours
{
    internal static class DetoursSignatures
    {
        #region Static Methods

        internal static IntPtr GetChangeGameRulesState() => MemoryManager.FindSignature(
            new byte[]
            {
                0x55, 0x8B, 0xEC, 0x83, 0xE4, 0xF8, 0x83, 0xEC, 0x14, 0x53, 0x56, 0x8B, 0x75, 0x08, 0x8B, 0xD9, 0x8B,
                0x0D, 0x00, 0x00, 0x00, 0x00
            },
            "xxxxxxxxxxxxxxxxxx????",
            "client.dll",
            0xef9000);

        internal static IntPtr GetChangeGameUiState() => MemoryManager.FindSignature(
            new byte[] {0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x14, 0x53, 0x8B, 0xD9, 0x56, 0x57, 0x8B, 0x7D, 0x08},
            "xxxxxxxxxxxxxx",
            "client.dll",
            0xef9000);

        internal static IntPtr GetAcceptLobby() => MemoryManager.FindSignature(
            new byte[] {0x66, 0xA1, 0xD0, 0xCF, 0xAC, 0x5D, 0x51, 0x8B, 0xCC, 0x66, 0x89, 0x01, 0x8B, 0xCF},
            "xxxxxxxxxxxxxx",
            "client.dll",
            0xf52000);

        #endregion
    }
}