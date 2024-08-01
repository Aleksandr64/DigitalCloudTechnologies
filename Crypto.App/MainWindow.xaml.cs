using System.Windows;
using System.Windows.Input;
using Crypto.App.ViewModels;
using Crypto.App.Views.Pages;

namespace Crypto.App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new MainPage());
    }
    
    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double screenWidth = SystemParameters.PrimaryScreenWidth;
        double screenHeight = SystemParameters.PrimaryScreenHeight;

        this.Width = screenWidth * 0.8;
        this.Height = screenHeight * 0.8;

        this.Left = (screenWidth - this.Width) / 2;
        this.Top = (screenHeight - this.Height) / 2;
    }
    
    private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 1)
        {
            DragMove();
        }
        else if (e.ClickCount == 2)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }
    }
}