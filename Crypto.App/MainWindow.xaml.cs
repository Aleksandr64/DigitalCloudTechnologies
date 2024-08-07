using System.Windows;
using Crypto.App.ViewModels;
using Crypto.App.Views.Components;
using Crypto.App.Views.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Crypto.App;

public partial class MainWindow : Window
{
    private readonly MainPage _mainPage;

    public MainWindow(MainPage mainPage, Header header)
    {
        InitializeComponent();
        _mainPage = mainPage;
        HeaderContentControl.Content = header;
        MainFrame.Navigate(_mainPage);
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