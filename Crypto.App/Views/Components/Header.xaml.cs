using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Crypto.App.Models;
using Crypto.App.Services;
using Crypto.App.ViewModels;
using Crypto.App.Views.Pages;
using Newtonsoft.Json.Linq;

namespace Crypto.App.Views.Components
{
    public partial class Header : UserControl
    {
        private readonly DispatcherTimer _searchDelayTimer;
        private readonly CryptoService _cryptoService;
        private HttpClient _httpClient;

        public Header()
        {
            InitializeComponent();
            _cryptoService = new CryptoService();
            DataContext = new HeaderViewModel();
            _searchDelayTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.3)
            };
            _searchDelayTimer.Tick += OnSearchDelayTimerTick;
            _httpClient = new HttpClient();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchDelayTimer.Stop();
            _searchDelayTimer.Start();
        }

        private void OnSearchDelayTimerTick(object sender, EventArgs e)
        {
            _searchDelayTimer.Stop();
            ExecuteSearch();
        }

        private async void ExecuteSearch()
        {
            var searchText = SearchTextBox.Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                var response = await _cryptoService.GetListSearchCryptoCurrency(searchText);

                var viewModel = (HeaderViewModel)DataContext;
                viewModel.SearchResults.Clear();
                if (response.Count > 0)
                {
                    foreach (var coin in response)
                    {
                        viewModel.SearchResults.Add(coin);
                    }

                    SearchResultsPopup.IsOpen = true;
                    SearchBorder.CornerRadius = new CornerRadius(15, 15, 0, 0);
                }
            }
            else
            {
                SearchBorder.CornerRadius = new CornerRadius(15);
                SearchResultsPopup.IsOpen = false;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            var viewModel = (HeaderViewModel)DataContext;
            viewModel.SearchResults.Clear();
            SearchBorder.CornerRadius = new CornerRadius(15);
            SearchResultsPopup.IsOpen = false;
        }

        private void SearchResultsPopup_OnClosed(object sender, EventArgs e)
        {
            if (!SearchResultsPopup.IsOpen)
            {
                SearchBorder.CornerRadius = new CornerRadius(15);
            }
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
            if (SearchResultsListBox.SelectedItem is CoinSearch selectedItem && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                SearchBorder.CornerRadius = new CornerRadius(15);
                SearchResultsPopup.IsOpen = false;
                var viewModel = (HeaderViewModel)DataContext;
                viewModel.SearchResults.Clear();
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.MainFrame.Navigate(new CurrencyDetailsPage(selectedItem.Id));
            }
        }
    }
}
