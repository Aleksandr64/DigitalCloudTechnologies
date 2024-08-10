namespace Crypto.App.Attribute;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class ErrorHandlerAttribute : System.Attribute
{
    public string ErrorMessage { get; }

    public ErrorHandlerAttribute(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}