using Avalonia.Media.Imaging;

namespace måleusikkerhet.ViewModels;

public class DeviceCardViewModel : ViewModelBase
{
    public DeviceCardViewModel(string name, Bitmap image)
    {
        Name = name;
        Image = image;
    }
    public DeviceCardViewModel(){}

    public string? Name { get; set; }
    public Bitmap? Image { get; set; }
}