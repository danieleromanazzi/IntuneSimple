using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace IntuneSimple
{
    public class WinApi
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hwnd, ref RectApi rectangle);

        public struct RectApi
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public static async Task<bool> Opened()
        {
            return await Task.Run<bool>(() =>
            {
                return FindWindowByCaption(IntPtr.Zero, Settings.UACViewTitle) != IntPtr.Zero;
            });
        }

        public static async Task<RectApi> GetUACRect()
        {
            return await Task.Run(() =>
            {
                RectApi rect = new RectApi();

                var ptr = FindWindowByCaption(IntPtr.Zero, Settings.UACViewTitle);

                SetForegroundWindow(ptr);

                GetWindowRect(ptr, ref rect);

                return rect;
            });
        }
    }
}
