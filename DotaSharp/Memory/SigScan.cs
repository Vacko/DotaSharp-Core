using System;
using System.Diagnostics;

namespace DotaSharp.Memory
{
    internal sealed class SigScan
    {
        #region Fields

        private byte[] m_vDumpedRegion;

        #endregion

        #region Constructors

        internal SigScan()
        {
            Process = null;
            Address = IntPtr.Zero;
            Size = 0;
            m_vDumpedRegion = null;
        }

        internal SigScan(Process proc, IntPtr addr, int size)
        {
            Process = proc;
            Address = addr;
            Size = size;
        }

        #endregion

        #region Properties

        private Process Process { get; }

        private IntPtr Address { get; }

        private int Size { get; }

        #endregion

        #region Internal Methods

        internal IntPtr FindPattern(byte[] btPattern, string strMask, int nOffset)
        {
            try
            {
                if (m_vDumpedRegion == null || m_vDumpedRegion.Length == 0)
                    if (!DumpMemory())
                        return IntPtr.Zero;

                if (strMask.Length != btPattern.Length)
                    return IntPtr.Zero;

                for (int x = 0; x < m_vDumpedRegion.Length; x++)
                    if (MaskCheck(x, btPattern, strMask))
                        return new IntPtr((int) Address + x + nOffset);

                return IntPtr.Zero;
            }
            catch (Exception)
            {
                return IntPtr.Zero;
            }
        }

        #endregion

        #region Private Methods

        private bool DumpMemory()
        {
            try
            {
                if (Process == null)
                    return false;
                if (Process.HasExited)
                    return false;
                if (Address == IntPtr.Zero)
                    return false;
                if (Size == 0)
                    return false;

                m_vDumpedRegion = new byte[Size];

                bool bReturn = false;

                bReturn = WinImports.ReadProcessMemory(
                    Process.Handle, Address, m_vDumpedRegion, Size, out int nBytesRead
                );

                if (bReturn == false || nBytesRead != Size)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool MaskCheck(int nOffset, byte[] btPattern, string strMask)
        {
            for (int x = 0; x < btPattern.Length; x++)
            {
                if (strMask[x] == '?')
                    continue;

                if (strMask[x] == 'x' && btPattern[x] != m_vDumpedRegion[nOffset + x])
                    return false;
            }

            return true;
        }

        #endregion
    }
}