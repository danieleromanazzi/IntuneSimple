using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace IntuneSimple
{
    public class Api
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public static async Task<bool> Opened()
        {
            var task = Task.Run<bool>(() =>
            {
                return FindWindowByCaption(IntPtr.Zero, "Controllo dell'account utente") != IntPtr.Zero;
            });

            return await task;
        }

        public static async Task<Rect> RectCredentialView()
        {
            var task = Task.Run<Rect>(() =>
            {
                var ptr = FindWindowByCaption(IntPtr.Zero, "Controllo dell'account utente");

                SetForegroundWindow(ptr);

                Rect rect = new Rect();
                GetWindowRect(ptr, ref rect);

                return rect;
            });

            return await task;
        }
    }
}
