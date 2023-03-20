using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using måleusikkerhet.ViewModels;

namespace måleusikkerhet.Views;

public partial class PreciseAttributeListWindow : Window
{
    public PreciseAttributeListWindow()
    {
        var model = new AttributeListWindowModel();
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}