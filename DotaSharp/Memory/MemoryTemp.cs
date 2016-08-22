using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace DotaSharp.Memory
{
    internal static class MemoryTemp
    {
        #region Fields

        private static readonly object SLock;
        private static readonly IntPtr SBuffer;
        private static uint m_sPointer;

        #endregion

        #region Constructors

        static MemoryTemp()
        {
            SLock = new object();
            SBuffer = Marshal.AllocHGlobal(2048);
            m_sPointer = 0U;
        }

        #endregion

        #region Static Methods

        internal static IntPtr GetTemporaryMemory(uint size)
        {
            Monitor.Enter(SLock);
            IntPtr num = SBuffer + (int) m_sPointer;
            m_sPointer += size;
            if (m_sPointer >= 2048U)
            {
                m_sPointer = 0U;
                num = SBuffer;
            }

            Monitor.Exit(SLock);
            return num;
        }

        #endregion
    }
}