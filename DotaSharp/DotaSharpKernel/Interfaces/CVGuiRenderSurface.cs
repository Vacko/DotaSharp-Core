using System;
using System.Runtime.InteropServices;

namespace DotaSharp.DotaSharpKernel.Interfaces
{
    [StructLayout(LayoutKind.Explicit)]
    internal class CvGuiRenderSurfaceVTable
    {
        #region Fields

        [FieldOffset(0x04 * 51)] public IntPtr CreateFont;

        [FieldOffset(0x04 * 24)] public IntPtr GetScreenSize;

        [FieldOffset(0x04 * 58)] public IntPtr SetFontGlyphSet;

        [FieldOffset(0x04 * 89)] public IntPtr SurfaceGetCursorPos;

        [FieldOffset(0x04 * 90)] public IntPtr SurfaceSetCursorPos;

        #endregion
    }

    [InterfaceVersions("VGUI_Surface032")]
    internal class CvGuiRenderSurface : InteropHelp.NativeWrapper<CvGuiRenderSurfaceVTable>
    {
        #region Fields

        private static CvGuiRenderSurface m_instance;
        private static readonly object Padlock = new object();

        #endregion

        #region Properties

        public static CvGuiRenderSurface Instance
        {
            get
            {
                lock (Padlock)
                {
                    return m_instance ??
                           (m_instance = Dota.CreateInterface<CvGuiRenderSurface>("vguirendersurface.dll"));
                }
            }
        }

        #endregion

        #region Public Methods

        public ulong CreateFont() => GetFunction<NativeCreateFont>(Functions.CreateFont)(ObjectAddress);

        public void GetScreenSize(ref int x, ref int y) =>
            GetFunction<NativeGetScreenSize>(Functions.GetScreenSize)(ObjectAddress, ref x, ref y);

        public unsafe void SetFontGlyphSet(ulong font, sbyte* windowFontName, int tall, int weight, EFontFlags flags) =>
            GetFunction<NativeSetFontGlyphSet>(Functions.SetFontGlyphSet)(ObjectAddress, (int) font, windowFontName,
                tall, weight, 0, 0, flags, 0, 0);

        public void SurfaceGetCursorPos(ref int x, ref int y) =>
            GetFunction<NativeSurfaceGetCursorPos>(Functions.SurfaceGetCursorPos)(ObjectAddress, ref x, ref y);

        public void SurfaceSetCursorPos(int x, int y) =>
            GetFunction<NativeSurfaceSetCursorPos>(Functions.SurfaceSetCursorPos)(ObjectAddress, x, y);

        #endregion

        #region Nested Types, Enums, Delegates

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeGetScreenSize(IntPtr @this, ref int x, ref int y);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate ulong NativeCreateFont(IntPtr @this);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private unsafe delegate void NativeSetFontGlyphSet(IntPtr @this, int font, sbyte* windowFontName, int tall,
            int weight, int blur, int scanlines, EFontFlags flags, int rangeMin, int rangeMax);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeSurfaceGetCursorPos(IntPtr @this, ref int x, ref int y);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NativeSurfaceSetCursorPos(IntPtr @this, int x, int y);

        #endregion
    }
}