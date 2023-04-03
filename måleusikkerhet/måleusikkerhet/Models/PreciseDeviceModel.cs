using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using måleusikkerhet.Bases;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Models;

public class PreciseDeviceModel : ModelBase
{

    private ObservableCollection<PreciseAttributesModel> _attributesModels = new();

    public ObservableCollection<PreciseAttributesModel> AttributesModels
    {
        get => _attributesModels;
        set => SetField(ref _attributesModels, value);
    }


}

public static class PreciseExtension
{
    public static PreciseDeviceModel ToModel(this Precise precise)
    {
        if (precise.Image?.ImageData != null)
            return new PreciseDeviceModel
            {
                Name = precise.Name, Image = ImageToBitmap.ToBitmap(precise.Image?.ImageData!),
                AttributesModels =
                    new ObservableCollection<PreciseAttributesModel>(precise.Ranges
                        .Select(PreciseAtributesToModel.ToModel).ToList())
            };
        return new PreciseDeviceModel();
    }
}