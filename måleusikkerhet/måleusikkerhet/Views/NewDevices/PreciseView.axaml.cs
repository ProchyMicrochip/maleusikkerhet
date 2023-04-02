using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Views.NewDevices;

public partial class PreciseView : UserControl
{
    public PreciseView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var window = new PreciseAttributeListWindow();
        window.Show();
    }
    
}