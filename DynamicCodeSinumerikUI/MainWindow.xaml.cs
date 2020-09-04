using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DynamicCodeSinumerikTest;

namespace DynamicCodeSinumerikUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
 if (SinumerikDynamicWrapper.Instance.SinumerikWrapper.CheckConnection())
            {
                Console.WriteLine("CheckConnection OK");
            }
            else
            {
                Console.WriteLine("CheckConnection Faild");

            }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
           
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SinumerikDynamicWrapper.Instance.GenerateClass();
            }
            catch (Exception exception)
            {
               MessageBox.Show(exception.Message);
            }
        }

        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport(@"kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport(@"user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SwHide = 0;
        const int SwShow = 5;


        public static void ShowConsoleWindow()
        {
            var handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();
            }
            else
            {
                ShowWindow(handle, SwShow);
            }
        }

        public static void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();

            ShowWindow(handle, SwHide);
        }

        private void OpenConsole(object sender, RoutedEventArgs e)
        {
            ShowConsoleWindow();
        }
    }
}
