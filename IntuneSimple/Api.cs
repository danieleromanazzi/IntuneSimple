using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace IntuneSimple
{

    //For enhancement see this
    //https://social.msdn.microsoft.com/Forums/vstudio/en-US/830e0068-725e-4066-8987-0489b7f1f433/how-to-get-the-position-of-a-control-of-separate-program?forum=csharpgeneral
    public class Api
    {

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private void AddA()
        {
            var ptr = FindWindowByCaption(IntPtr.Zero, "Nuovo documento di testo (2).txt - Blocco Note");
            SetForegroundWindow(ptr);

            System.Windows.Forms.SendKeys.SendWait(Clipboard.GetText());
        }
    }
}
