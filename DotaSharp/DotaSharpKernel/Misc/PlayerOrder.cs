using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Misc
{
    internal static class PlayerOrder
    {
        #region Static Methods

        internal static void PrepareUnitOrders(IntPtr entityPointer, int orderToExecute, int targetIndex, float x,
            float y, float z, int abilityIndex, OrderIssuer orderIsuer, Hero abilityCaster, bool unkn1, bool unkn2)
        {
            IntPtr abilityCasterPtr = IntPtr.Zero;

            IntPtr getPrepareUnitOrdersPtr = Signatures.SignatureGetPrepareUnitOrders;

            if (getPrepareUnitOrdersPtr == IntPtr.Zero)
                return;

            if (abilityCaster != null)
                abilityCasterPtr = abilityCaster.BasePointer();

            NativePrepareUnitOrders getPrepareUnitOrdersFunction =
                (NativePrepareUnitOrders) Marshal.GetDelegateForFunctionPointer(getPrepareUnitOrdersPtr,
                    typeof(NativePrepareUnitOrders));

            getPrepareUnitOrdersFunction(entityPointer, orderToExecute, targetIndex, x, y, z, abilityIndex,
                (int) orderIsuer, abilityCasterPtr, unkn1, unkn2);
        }

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativePrepareUnitOrders(IntPtr @this, int order, int targetIndex, float x, float y,
            float z, int abilityIndex, int orderIsuer, IntPtr abilityCaster, bool unkn1, bool unkn2);

        #endregion
    }
}