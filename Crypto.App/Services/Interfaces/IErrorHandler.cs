namespace Crypto.App.Services.Interfaces;

public interface IErrorHandler
{
    Task HandleErrorAsync(Exception ex, string message = null);
}