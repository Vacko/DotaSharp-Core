using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Interfaces
{
    [StructLayout(LayoutKind.Explicit)]
    internal class CvGuiPaintSurfaceMainThreadVTable
    {
        #region Fields

        [FieldOffset(0x04 * 7)] public IntPtr DrawColoredCircle;

        [FieldOffset(0x04 * 19)] public IntPtr DrawFilledRect;

        [FieldOffset(0x04 * 22)] public IntPtr DrawLine;

        [FieldOffset(0x04 * 21)] public IntPtr DrawOutlinedRect;

        [FieldOffset(0x04 * 27)] public IntPtr DrawPrintText;

        [FieldOffset(0x04 * 17)] public IntPtr DrawSetColor;

        [FieldOffset(0x04 * 25)] public IntPtr DrawSetTextColor;

        [FieldOffset(0x04 * 13)] public IntPtr DrawSetTextFont;

        [FieldOffset(0x04 * 26)] public IntPtr DrawSetTextPos;

        #endregion
    }

    [InterfaceVersions("VGUI_Surface032")]
    internal class CvGuiPaintSurfaceMainThread : InteropHelp.NativeWrapper<CvGuiPaintSurfaceMainThreadVTable>
    {
        #region Fields

        private static CvGuiPaintSurfaceMainThread m_instance;
        private static readonly object Padlock = new object();

        #endregion

        #region Properties

        public static CvGuiPaintSurfaceMainThread Instance
        {
            get
            {
                lock (Padlock)
                {
                    return m_instance ?? (m_instance =
                               Dota.CreateInterface<CvGuiPaintSurfaceMainThread>("vguirendersurface.dll", 0x130));
                }
            }
        }

        #endregion

        #region Public Methods

        public void DrawColoredCircle(int x, int y, float w, int r, int g, int b, int a) =>
            GetFunction<NativeDrawColoredCircle>(Functions.DrawColoredCircle)(ObjectAddress, x, y, w, r, g, b, a);

        public void DrawFilledRect(int x0, int y0, int x1, int y1) =>
            GetFunction<NativeDrawFilledRect>(Functions.DrawFilledRect)(ObjectAddress, x0, y0, x1, y1);

        public void DrawLine(int x0, int y0, int x1, int y1) =>
            GetFunction<NativeDrawLine>(Functions.DrawLine)(ObjectAddress, x0, y0, x1, y1);

        public void DrawOutlinedRect(int x0, int y0, int x1, int y1) =>
            GetFunction<NativeDrawOutlinedRect>(Functions.DrawOutlinedRect)(ObjectAddress, x0, y0, x1, y1);

        public void DrawPrintText([MarshalAs(UnmanagedType.LPWStr)] string text, int length) =>
            GetFunction<NativeDrawPrintText>(Functions.DrawPrintText)(ObjectAddress, text, length, 0);

        public void DrawSetColor(int r, int g, int b, int a) =>
            GetFunction<NativeDrawSetColor>(Functions.DrawSetColor)(ObjectAddress, r, g, b, a);

        public void DrawSetTextColor(int r, int g, int b, int a) =>
            GetFunction<NativeDrawSetTextColor>(Functions.DrawSetTextColor)(ObjectAddress, r, g, b, a);

        public void DrawSetTextFont(ulong font) =>
            GetFunction<NativeDrawSetTextFont>(Functions.DrawSetTextFont)(ObjectAddress, font);

        public void DrawSetTextPos(int x, int y) =>
            GetFunction<NativeDrawSetTextPos>(Functions.DrawSetTextPos)(ObjectAddress, x, y);

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeDrawColoredCircle(IntPtr @this, int x, int y, float w, int r, int g, int b, int a);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeDrawSetTextFont(IntPtr @this, ulong font);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeDrawSetColor(IntPtr @this, int r, int g, int b, int a);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeDrawFilledRect(IntPtr @this, int x0, int y0, int x1, int y1);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeDrawOutlinedRect(IntPtr @this, int x0, int y0, int x1, int y1);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeDrawLine(IntPtr thisptr, int x0, int y0, int x1, int y1);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeDrawSetTextColor(IntPtr @this, int r, int g, int b, int a);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeDrawSetTextPos(IntPtr @this, int x, int y);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeDrawPrintText(IntPtr @this, [MarshalAs(UnmanagedType.LPWStr)] string text, int len,
            int I);

        #endregion
    }
}