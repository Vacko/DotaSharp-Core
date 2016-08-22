using System;
using System.Runtime.InteropServices;
using DotaSharp.DotaSharpKernel.Misc;

namespace DotaSharp.DotaSharpKernel.EntitySystem
{
    internal static class EntitySystem
    {
        #region Static Methods

        internal static IntPtr GetLocalPlayer()
        {
            IntPtr localPlayerPtr = Signatures.SignatureGetLocalPlayer;

            if (localPlayerPtr == IntPtr.Zero)
                return IntPtr.Zero;

            NativeGetLocalPlayer getLocalPlayerFunction =
                (NativeGetLocalPlayer) Marshal.GetDelegateForFunctionPointer(localPlayerPtr,
                    typeof(NativeGetLocalPlayer));

            return getLocalPlayerFunction(IntPtr.Zero);
        }

        internal static IntPtr NextEnt(IntPtr baseEntity)
        {
            IntPtr nextEntPtr = Signatures.SignatureGetNextEnt;

            if (nextEntPtr == IntPtr.Zero || EntitySystemInstance() == IntPtr.Zero)
                return IntPtr.Zero;

            NativeNextEnt nextEntFunction =
                (NativeNextEnt) Marshal.GetDelegateForFunctionPointer(nextEntPtr, typeof(NativeNextEnt));

            return nextEntFunction(EntitySystemInstance(), baseEntity);
        }

        internal static IntPtr GetEntityFromHandle(int entityHandle) => GetEntityByIndex(HandleToIndex(entityHandle));

        internal static IntPtr GetEntityByIndex(int index)
        {
            IntPtr getEntityByIndexPtr = Signatures.SignatureGetEntityByIndex;

            if (getEntityByIndexPtr == IntPtr.Zero || EntitySystemInstance() == IntPtr.Zero)
                return IntPtr.Zero;

            NativeGetEntityByIndex getEntitybyIndexFunction =
                (NativeGetEntityByIndex) Marshal.GetDelegateForFunctionPointer(getEntityByIndexPtr,
                    typeof(NativeGetEntityByIndex));

            return getEntitybyIndexFunction(EntitySystemInstance(), index);
        }

        internal static int GetEntityIndex(IntPtr entity)
        {
            IntPtr getEntityIndexPtr = Signatures.SignatureGetEntityIndex;

            if (getEntityIndexPtr == IntPtr.Zero)
                return 0;

            NativeGetEntityIndex getEntityIndexFunction =
                (NativeGetEntityIndex) Marshal.GetDelegateForFunctionPointer(getEntityIndexPtr,
                    typeof(NativeGetEntityIndex));

            int getIndex = 0;
            getEntityIndexFunction(entity, ref getIndex);

            return getIndex;
        }

        internal static int HandleToIndex(int handle) => handle & short.MaxValue;
        private static IntPtr EntitySystemInstance() => Signatures.CSource2GetEntitySystem;

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetLocalPlayer(IntPtr @this);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeNextEnt(IntPtr @this, IntPtr baseEntity);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativeGetEntityByIndex(IntPtr @this, int index);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int NativeGetEntityIndex(IntPtr @this, ref int index);

        #endregion
    }
}