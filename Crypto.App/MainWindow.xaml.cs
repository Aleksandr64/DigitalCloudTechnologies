using System.Windows;
using Crypto.App.Views.Pages;

namespace Crypto.App;

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
        this.Height = screenHeight * 0.75;

        this.Left = (screenWidth - this.Width) / 2;
        this.Top = (screenHeight - this.Height) / 2;
    }
}