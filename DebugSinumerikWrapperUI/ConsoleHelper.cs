using System;

namespace DebugSinumerikWrapperUI
{
    public static partial class ConsoleHelper
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
    }
}