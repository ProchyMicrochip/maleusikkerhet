using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using måleusikkerhet.ViewModels;

namespace måleusikkerhet.Views;

public partial class AddAnalogDeviceForm : Window
{
    private readonly AddAnalogDeviceFormViewModel _model = new();
    public AddAnalogDeviceForm()
    {
        InitializeComponent();
        DataContext = _model;
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void LoadImage(object? sender, RoutedEventArgs e)
    {
        _model.LoadImage();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        _model.Save();
    }
}