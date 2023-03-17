using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
}