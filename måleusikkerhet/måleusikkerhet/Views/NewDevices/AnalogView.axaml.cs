using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace måleusikkerhet.Views.NewDevices;

public partial class AnalogView : UserControl
{
    public AnalogView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}