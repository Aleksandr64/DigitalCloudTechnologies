using System.Windows;
using Crypto.App.Services.Interfaces;

namespace Crypto.App.Services;

public class ErrorHandler : IErrorHandler
{
    public async Task HandleErrorAsync(Exception ex, string message = null)
    {
        await ShowErrorMessageAsync(ex, message);
    }

    private Task ShowErrorMessageAsync(Exception ex, string? message)
    {
        return Task.Run(() =>
        {
            string errorMessage = message ?? "An unexpected error occurred.";
            MessageBox.Show($"{errorMessage}\n\nDetails: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        });
    }
}