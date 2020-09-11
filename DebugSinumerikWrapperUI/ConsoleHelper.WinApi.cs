using System;
using System.Runtime.InteropServices;

namespace DebugSinumerikWrapperUI
{
    public static partial class ConsoleHelper
    {
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