using System;
using System.Runtime.InteropServices;
using DotaSharp.DotaSharpKernel.Models;
using DotaSharp.DotaSharpKernel.Offsets;
using DotaSharp.Memory;

namespace DotaSharp.DotaSharpKernel.Misc
{
    internal static class ReadObj
    {
        #region Static Methods

        internal static string ReadClassFromPointer(IntPtr entityPointer)
        {
            if (entityPointer == IntPtr.Zero)
                return string.Empty;

            IntPtr getClassIdPointer = MemoryManager.Function(entityPointer, EntityVTable.GetClientClass);

            NativeGetClientClass getClassIdFunction =
                (NativeGetClientClass) Marshal.GetDelegateForFunctionPointer(getClassIdPointer,
                    typeof(NativeGetClientClass));

            IntPtr classId = getClassIdFunction(entityPointer);

            NetVars clientClass = (NetVars) Marshal.PtrToStructure(classId, typeof(NetVars));

            return MemoryManager.ReadString(clientClass.m_ServerName);
        }

        internal static string ReadNameFromPointer(IntPtr entityPointer)
        {
            if (entityPointer == IntPtr.Zero)
                return string.Empty;

            IntPtr dynamicBindingPointer = MemoryManager.Function(entityPointer, EntityVTable.DynamicBinding);

            NativeDynamicBinding dynamicBindingFunction =
                (NativeDynamicBinding) Marshal.GetDelegateForFunctionPointer(dynamicBindingPointer,
                    typeof(NativeDynamicBinding));

            IntPtr dynamicBinding = dynamicBindingFunction(entityPointer);

            IntPtr getName = MemoryManager.ReadIntPtr(dynamicBinding + 0xC);

            return MemoryManager.ReadString(getName);
        }

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetClientClass(IntPtr @this);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeDynamicBinding(IntPtr @this);

        #endregion
    }
}