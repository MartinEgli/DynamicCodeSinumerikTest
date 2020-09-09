using System;
using System.Windows;
using DynamicSinumerikWrapper;
using StaticSinumerikWrapper;

namespace DynamicSinumerikWrapperUI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestConnectionDynamicWrapperClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DynamicSinumerikWrapperProvider.Instance.SinumerikWrapper.CheckConnection())
                    Console.WriteLine("CheckConnection OK");
                else
                    Console.WriteLine("CheckConnection Faild");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void TestConnectionStaticWrapperClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StaticSinumerikWrapperProvider.Instance.SinumerikWrapper.CheckConnection())
                    Console.WriteLine("CheckConnection OK");
                else
                    Console.WriteLine("CheckConnection Faild");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DynamicSinumerikWrapperProvider.Instance.GenerateClass();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void OpenConsole(object sender, RoutedEventArgs e)
        {
           ConsoleHelper.ShowConsoleWindow();
        }
    }
}