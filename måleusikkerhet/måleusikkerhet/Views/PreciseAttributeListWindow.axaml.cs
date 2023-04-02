using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;
using måleusikkerhet.ViewModels;

namespace måleusikkerhet.Views;

public partial class PreciseAttributeListWindow : Window
{
    private AttributeListWindowModel _model;


    public PreciseAttributeListWindow()
    {
        _model = new AttributeListWindowModel();
        _model.Invalidate += InvalidateVisual;
        _model.OnClose += CloseWindow;
        DataContext = _model;
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

    }

    private void CloseWindow()
    {
        Close();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}