using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Crypto.App.Views.Pages;

namespace Crypto.App.Views.Components
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }

        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
        
        private void HeaderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.DragMove();
            }
        }
    }
}