using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using måleusikkerhet.ViewModels;

namespace måleusikkerhet.Views;

public partial class DeviceCardView : UserControl
{
    public DeviceCardView(string name, Bitmap image)
    {
        InitializeComponent();
        DataContext = new DeviceCardViewModel(name,image);
    }

    public DeviceCardView()
    {
        InitializeComponent();
        DataContext = new DeviceCardViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}