using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace DotaSharp.Memory
{
    internal static class MemoryManager
    {
        #region Static Methods

        internal static unsafe sbyte* StringToChar(string stringToChar)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(stringToChar);
            IntPtr temporaryMemory = MemoryTemp.GetTemporaryMemory((uint) (bytes.Length + 1));
            IntPtr ptr = temporaryMemory;
            Marshal.Copy(bytes, 0, temporaryMemory, bytes.Length);
            Marshal.WriteByte(ptr, bytes.Length, 0);

            return (sbyte*) ptr.ToPointer();
        }

        internal static IntPtr FindSignature(byte[] signature, string mask, string module, int size, int offset = 0)
        {
            SigScan scanMemory = new SigScan(Process.GetCurrentProcess(), GetModule(module), size);

            return scanMemory.FindPattern(signature, mask, offset);
        }

        internal static IntPtr Function(IntPtr pointer, int index = 0) =>
            ReadIntPtr(ReadIntPtr(pointer) + index * 0x04);

        internal static int ReadInt(IntPtr offset, int bytesToRead = 4) =>
            BitConverter.ToInt32(Read(offset, bytesToRead, out int _), 0);

        internal static IntPtr ReadIntPtr(IntPtr offset) => new IntPtr(ReadInt(offset));

        internal static float ReadFloat(IntPtr offset, int bytesToRead = 4) =>
            BitConverter.ToSingle(Read(offset, bytesToRead, out int _), 0);

        internal static string ReadString(IntPtr offset, int bytesToRead = 128)
        {
            int endIndex = 0;
            byte[] buf = new byte[bytesToRead];
            buf = Read(offset, bytesToRead, out int _);

            for (int i = 0; i < buf.Length; i++)
                if (buf[i] == '\0')
                {
                    endIndex = i;
                    break;
                }

            return Encoding.ASCII.GetString(buf, 0, endIndex);
        }

        private static IntPtr GetModule(string processModule)
        {
            ProcessModule module = null;

            foreach (ProcessModule proc in Process.GetCurrentProcess().Modules)
                if (proc.ModuleName == processModule)
                    module = proc;

            return WinImports.LoadLibraryEx(module.FileName, IntPtr.Zero,
                WinImports.LoadLibraryFlags.DontResolveDllReferences);
        }

        private static byte[] Read(IntPtr memoryAddress, int bytesToRead, out int bytesRead)
        {
            byte[] buffer = new byte[bytesToRead];
            WinImports.ReadProcessMemory(Process.GetCurrentProcess().Handle, memoryAddress, buffer, bytesToRead,
                out int ptrBytesRead);
            bytesRead = ptrBytesRead;

            return buffer;
        }

        #endregion
    }
}