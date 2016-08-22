using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Models
{
    [StructLayout(LayoutKind.Explicit)]
    internal class NetVars
    {
        #region Fields

        [FieldOffset(0x24)] public int m_ClassId;

        [FieldOffset(0x04)] public IntPtr m_ClientName;

        [FieldOffset(0x08)] public IntPtr m_Next;

        [FieldOffset(0x00)] public IntPtr m_ServerName;

        #endregion
    }
}