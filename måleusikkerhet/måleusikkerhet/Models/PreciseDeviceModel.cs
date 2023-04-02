using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Models;

public class PreciseDeviceModel : ModelBase
{
    private string _name = string.Empty;
    private Bitmap? _image;
    private ObservableCollection<PreciseAttributesModel> _attributesModels = new();

    public ObservableCollection<PreciseAttributesModel> AttributesModels
    {
        get => _attributesModels;
        set => SetField(ref _attributesModels, value);
    }

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public Bitmap? Image
    {
        get => _image;
        set => SetField(ref _image, value);
    }
    
}