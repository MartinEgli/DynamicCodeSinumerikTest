using System;
using System.Runtime.InteropServices;

namespace DynamicSinumerikWrapperUI
{
    public class ConsoleHelper
    {
        public static void HideConsoleWindow()
        {
            var handle = WinApi.GetConsoleWindow();

            WinApi.ShowWindow(handle, WinApi.SwHide);
        }

        public static void ShowConsoleWindow()
        {
            var handle = WinApi.GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                WinApi.AllocConsole();
            }
            else
            {
                WinApi.ShowWindow(handle, WinApi.SwShow);
            }
        }
        

        private static class WinApi
        {
            public const int SwHide = 0;
            public const int SwShow = 5;

            [DllImport(@"kernel32.dll", SetLastError = true)]
            public static extern bool AllocConsole();

            [DllImport(@"kernel32.dll")]
            public static extern IntPtr GetConsoleWindow();

            [DllImport(@"user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        }
    }
}