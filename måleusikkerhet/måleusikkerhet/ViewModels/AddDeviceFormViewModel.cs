using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace måleusikkerhet.ViewModels;

public class AddDeviceFormViewModel : ViewModelBase
{
    private Bitmap? _image;

    public Bitmap? Image
    {
        get => _image;
        set => this.RaiseAndSetIfChanged(ref _image, value);
    }

    public async void LoadImage()
    {
        var fileDialog = new OpenFileDialog {AllowMultiple = false};
        var file = await fileDialog.ShowAsync(new Window());
        if(file?[0] == null)return;
        var bitmap = new Bitmap(file[0]);
        bitmap = bitmap.CreateScaledBitmap(new PixelSize(128, 128));
        Image = bitmap;
    }
}