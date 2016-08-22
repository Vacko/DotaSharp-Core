using System;
using System.Runtime.InteropServices;
using DotaSharp.Memory;

namespace DotaSharp.Hooking
{
    internal static class HookMemory
    {
        #region Static Methods

        public static byte[] Read(IntPtr address, int length)
        {
            byte[] dest = new byte[length];
            Marshal.Copy(address, dest, 0, length);
            return dest;
        }

        public static void Write(IntPtr address, byte[] value)
        {
            WinImports.VirtualProtect(address, (uint) value.Length, 0x40, out uint oldProtect);
            Marshal.Copy(value, 0, address, value.Length);
            WinImports.VirtualProtect(address, (uint) value.Length, oldProtect, out oldProtect);
        }

        #endregion
    }
}