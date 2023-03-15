using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using måleusikkerhet.Bases;
using måleusikkerhet.Database;

namespace måleusikkerhet.Views;

public partial class DeviceView : UserControl
{
    public DeviceView()
    {
        InitializeComponent();
        var panel = this.FindControl<WrapPanel>("WrapPanel")!;
        var db = new DeviceDb();
        var devices = db.AnalogDev.Select(x => x as DevBase).ToList().Concat(db.DigitalDev.Select(x => x as DevBase).ToList())
            .Concat(db.PreciseDev.Select(x => x as DevBase)).OrderBy(x => x.Name).Select(x =>
            {
                using var ms = new MemoryStream();
                ms.Write(x.Image?.ImageData);
                return new DeviceCardView(x.Name, Bitmap.DecodeToHeight(ms, 128));
            });
        panel.Children.AddRange(devices);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}