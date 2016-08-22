using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Misc
{
    internal static class Screen
    {
        #region Static Methods

        internal static int WorldToScreenX(float x, float y, float z)
        {
            IntPtr worldToScreenXPtr = Signatures.SignatureWorldToScreenX;

            if (worldToScreenXPtr == IntPtr.Zero)
                return 0;

            NativeWorldToScreenX worldToScreenXFunction =
                (NativeWorldToScreenX) Marshal.GetDelegateForFunctionPointer(worldToScreenXPtr,
                    typeof(NativeWorldToScreenX));

            return worldToScreenXFunction(x, y, z);
        }

        internal static int WorldToScreenY(float x, float y, float z)
        {
            IntPtr worldToScreenYPtr = Signatures.SignatureWorldToScreenY;

            if (worldToScreenYPtr == IntPtr.Zero)
                return 0;

            NativeWorldToScreenY worldToScreenYFunction =
                (NativeWorldToScreenY) Marshal.GetDelegateForFunctionPointer(worldToScreenYPtr,
                    typeof(NativeWorldToScreenY));

            return worldToScreenYFunction(x, y, z);
        }

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int NativeWorldToScreenX(float x, float y, float z);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int NativeWorldToScreenY(float x, float y, float z);

        #endregion
    }
}