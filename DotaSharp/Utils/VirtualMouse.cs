using System.Runtime.InteropServices;
using System.Threading;

namespace DotaSharp.Utils
{
    internal enum MouseEvents
    {
        MouseeventfLeftdown = 0x0002,
        MouseeventfLeftup = 0x0004,
        MouseeventfRightdown = 0x0008,
        MouseeventfRightup = 0x0010
    }

    internal static class VirtualMouse
    {
        #region Static Methods

        public static void LeftClick(int x, int y)
        {
            mouse_event((int) MouseEvents.MouseeventfLeftdown, x, y, 0, 0);
            Thread.Sleep(29);
            mouse_event((int) MouseEvents.MouseeventfLeftup, x, y, 0, 0);
        }

        public static void RightClick(int x, int y)
        {
            mouse_event((int) MouseEvents.MouseeventfRightdown, x, y, 0, 0);
            Thread.Sleep(29);
            mouse_event((int) MouseEvents.MouseeventfRightup, x, y, 0, 0);
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        #endregion
    }
}