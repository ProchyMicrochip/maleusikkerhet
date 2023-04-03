using System.Collections.ObjectModel;
using System.Linq;
using måleusikkerhet.Bases;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Models;

public class AnalogDeviceModel : ModelBase
{
    private ObservableCollection<AnalogAttributesModel> _attributesModels = new();
    private decimal _relativeRange;

    public decimal RelativeRange
    {
        get => _relativeRange;
        set => SetField(ref _relativeRange, value);
    }

    public ObservableCollection<AnalogAttributesModel> AttributesModels
    {
        get => _attributesModels;
        set => SetField(ref _attributesModels, value);
    }
}

public static class AnalogExtension
{
    public static AnalogDeviceModel ToModel(this Analog analog)
    {
        if (analog.Image?.ImageData != null)
            return new AnalogDeviceModel
            {
                Name = analog.Name, Image = ImageToBitmap.ToBitmap(analog.Image?.ImageData!),
                RelativeRange = analog.RelativeRange,
                AttributesModels =
                    new ObservableCollection<AnalogAttributesModel>(analog.Ranges
                        .Select(AnalogAtributesToModel.ToModel).ToList())
            };
        return new AnalogDeviceModel();
    }
}