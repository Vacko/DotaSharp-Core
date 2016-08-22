using System;
using System.Runtime.InteropServices;
using DotaSharp.DotaSharpKernel.Interfaces;
using DotaSharp.Memory;
using DotaSharp.Utils;

namespace DotaSharp.DotaSharpKernel.Detours.DetouredFunctions
{
    internal static class PaintTraverse
    {
        #region Fields

        private static ulong m_renderSystemTopPanel;

        #endregion

        #region Static Methods

        internal static void CreateHook()
        {
            IntPtr paintTraverseFunction =
                MemoryManager.Function(VPanelWrapper.Instance.Interface, VPanelWrapperVTable.PaintTraverse);

            if (paintTraverseFunction != IntPtr.Zero)
            {
                NativePaintTraverse originalFunction =
                    (NativePaintTraverse) Marshal.GetDelegateForFunctionPointer(paintTraverseFunction,
                        typeof(NativePaintTraverse));
                NativePaintTraverse newFunction = Hooked_PaintTraverse;

                DetoursMgr.GetDetours.Create(originalFunction, newFunction, "VPanelWrapper::PaintTraverse");

                Logging.Write(false, $"PaintTraverse")("PaintTraverse() has been Successfull");
            }
            else
            {
                Logging.Write(true, $"PaintTraverse")("PaintTraverse() not Found");
            }
        }

        private static IntPtr Hooked_PaintTraverse(IntPtr @this, IntPtr ivGuiPaintSurface, ulong vGuiPanel,
            bool forceRepaint)
        {
            if (m_renderSystemTopPanel == 0)
            {
                string panelName = VPanelWrapper.Instance.GetPanelName(vGuiPanel);

                if (panelName[0] == 'R' && panelName[6] == 'S') //RenderSystemTopPanel
                {
                    m_renderSystemTopPanel = vGuiPanel;

                    Logging.Write(false)($"Panel '{panelName}' has been found");

                    //Create Default Font for Drawing
                    Drawing.DefaultFont = Drawing.CreateFont("Arial Black", 15, 500, EFontFlags.FontflagCustom);
                }
            }

            if (m_renderSystemTopPanel == vGuiPanel) //Active OnDraw Events
                Drawing.OnDrawNative();

            return (IntPtr) DetoursMgr.GetDetours["VPanelWrapper::PaintTraverse"]
                .CallOriginal(@this, ivGuiPaintSurface, vGuiPanel, forceRepaint); //Returns pointer to p_Surface
        }

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr NativePaintTraverse(IntPtr @this, IntPtr ivGuiPaintSurface, ulong vGuiPanell,
            bool forceRepaint);

        #endregion
    }
}