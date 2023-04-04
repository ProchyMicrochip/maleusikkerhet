using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace måleusikkerhet.Views;

public partial class DataTable : UserControl
{
    public DataTable()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}