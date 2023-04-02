using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using måleusikkerhet.Services;
using Splat;

namespace måleusikkerhet.Views;

public partial class DeviceView : Window
{
    public DeviceView()
    {
        InitializeComponent();
        var service = Locator.Current.GetService<DatabaseService>();
        var panel = this.FindControl<WrapPanel>("WrapPanel")!;
        panel.Children.AddRange(service.Devices);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}