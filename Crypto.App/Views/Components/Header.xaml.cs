using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Crypto.App.ViewModels;

namespace Crypto.App.Views.Components;

public partial class Header : UserControl
{
    public Header()
    {
        
    }
    public Header(HeaderViewModel headerViewModel)
    {
        InitializeComponent();
        DataContext = headerViewModel;
    }

    private void HeaderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Window window = Window.GetWindow(this);
        if (window != null)
        {
            window.DragMove();
        }
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
    
    private void SearchResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataContext is HeaderViewModel viewModel)
        {
            viewModel.OnSelectionChanged(SearchResultsListBox.SelectedItem);
        }
    }
}

