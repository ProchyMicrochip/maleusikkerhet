using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace måleusikkerhet.Views;

public partial class AttributeListWindow : Window
{
    public AttributeListWindow()
    {
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