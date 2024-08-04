using System;
using System.Windows;
using System.Windows.Controls;

namespace Crypto.App.Utilities;

public class PlaceholderTextBox : TextBox
{
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register("Placeholder", typeof(string), typeof(PlaceholderTextBox),
            new PropertyMetadata(string.Empty));

    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set { SetValue(PlaceholderProperty, value); }
    }

    public static readonly DependencyProperty IsEmptyProperty =
        DependencyProperty.Register("IsEmpty", typeof(bool), typeof(PlaceholderTextBox),
            new PropertyMetadata(false));

    public bool IsEmpty
    {
        get { return (bool)GetValue(IsEmptyProperty); }
        private set { SetValue(IsEmptyProperty, value); }
    }

    static PlaceholderTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox), new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
    }

    protected override void OnInitialized(EventArgs e)
    {
        UpdateIsEmpty();
        base.OnInitialized(e);
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        UpdateIsEmpty();
        base.OnTextChanged(e);
    }

    private void UpdateIsEmpty()
    {
        IsEmpty = string.IsNullOrEmpty(Text);
    }
}
