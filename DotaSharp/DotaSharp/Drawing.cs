using System;
using System.Collections.Generic;
using DotaSharp.DotaSharp.Utils;
using DotaSharp.DotaSharpKernel.Interfaces;
using DotaSharp.DotaSharpKernel.Misc;
using DotaSharp.Memory;
using DotaSharp.Utils;

namespace DotaSharp
{
    public static class Drawing
    {
        #region Fields

        public static ulong DefaultFont;
        private static readonly List<DrawingOnDraw> OnDrawHandlers;

        #endregion

        #region Constructors

        static Drawing() => OnDrawHandlers = new List<DrawingOnDraw>();

        #endregion

        #region Properties

        public static int Width
        {
            get
            {
                int x = 0;
                int y = 0;

                CvGuiRenderSurface.Instance.GetScreenSize(ref x, ref y);

                return x;
            }
        }

        public static int Height
        {
            get
            {
                int x = 0;
                int y = 0;

                CvGuiRenderSurface.Instance.GetScreenSize(ref x, ref y);

                return y;
            }
        }

        #endregion

        #region Other

        public static event DrawingOnDraw OnDraw
        {
            add => OnDrawHandlers.Add(value);
            remove => OnDrawHandlers.Remove(value);
        }

        #endregion

        #region Static Methods

        public static ulong CreateFont(string windowFontName, int tall, int weight, EFontFlags flags)
        {
            ulong font = CreateFont();

            SetFontGlyphSet(font, windowFontName, tall, weight, flags);

            return font;
        }

        public static void DrawText(int x, int y, string text, Color color, ulong font)
        {
            CvGuiPaintSurfaceMainThread.Instance.DrawSetTextPos(x, y);
            CvGuiPaintSurfaceMainThread.Instance.DrawSetTextFont(font);
            CvGuiPaintSurfaceMainThread.Instance.DrawSetTextColor(color.R, color.G, color.B, color.A);
            CvGuiPaintSurfaceMainThread.Instance.DrawPrintText(text, text.Length);
        }

        public static void DrawText(int x, int y, string text, Color color) => DrawText(x, y, text, color, DefaultFont);

        public static void GetCursor(ref int x, ref int y) =>
            CvGuiRenderSurface.Instance.SurfaceGetCursorPos(ref x, ref y);

        public static void SetCursor(int x, int y) => CvGuiRenderSurface.Instance.SurfaceSetCursorPos(x, y);

        public static void DrawOutlineRect(int x, int y, int w, int h, Color color)
        {
            CvGuiPaintSurfaceMainThread.Instance.DrawSetColor(color.R, color.G, color.B, color.A);
            CvGuiPaintSurfaceMainThread.Instance.DrawOutlinedRect(x, y, x + w, y + h);
        }

        public static void DrawFilledRect(int x, int y, int w, int h, Color color)
        {
            CvGuiPaintSurfaceMainThread.Instance.DrawSetColor(color.R, color.G, color.B, color.A);
            CvGuiPaintSurfaceMainThread.Instance.DrawFilledRect(x, y, x + w, y + h);
        }

        public static void DrawLine(int x0, int y0, int x1, int y1, Color color)
        {
            CvGuiPaintSurfaceMainThread.Instance.DrawSetColor(color.R, color.G, color.B, color.A);
            CvGuiPaintSurfaceMainThread.Instance.DrawLine(x0, y0, x1, y1);
        }


        public static void DrawColoredCircle(int x, int y, float w, Color color) =>
            CvGuiPaintSurfaceMainThread.Instance.DrawColoredCircle(x, y, w, color.R, color.G, color.B, color.A);

        public static bool WorldToScreen(Vector3 world, out Vector2 screen)
        {
            screen.X = Screen.WorldToScreenX(world.X, world.Y, world.Z);
            screen.Y = Screen.WorldToScreenY(world.X, world.Y, world.Z);

            return screen.X != -1 && screen.Y != -1;
        }


        internal static void OnDrawNative()
        {
            try
            {
                List<DrawingOnDraw>.Enumerator enumerator = OnDrawHandlers.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DrawingOnDraw current = enumerator.Current;
                    try
                    {
                        current();
                    }
                    catch (Exception ex)
                    {
                        Logging.Write(true)("Failed Current Event OnDraw - " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Write(true)("Failed Events OnDraw - " + ex.Message);
            }
        }

        private static ulong CreateFont() => CvGuiRenderSurface.Instance.CreateFont();

        private static unsafe void SetFontGlyphSet(ulong font, string windowFontName, int tall, int weight,
            EFontFlags flags)
        {
            sbyte* fontName = MemoryManager.StringToChar(windowFontName);

            CvGuiRenderSurface.Instance.SetFontGlyphSet(font, fontName, tall, weight, flags);
        }

        #endregion
    }
}