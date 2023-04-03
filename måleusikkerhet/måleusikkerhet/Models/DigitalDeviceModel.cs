using System.Collections.ObjectModel;
using System.Linq;
using måleusikkerhet.Bases;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Models;

public class DigitalDeviceModel : ModelBase
{
    private ObservableCollection<DigitalAttributesModel> _attributesModels = new();

    public ObservableCollection<DigitalAttributesModel> AttributesModels
    {
        get => _attributesModels;
        set => SetField(ref _attributesModels, value);
    }

}
public static class DigitalExtension
{
    public static DigitalDeviceModel ToModel(this Digital precise)
    {
        if (precise.Image?.ImageData != null)
            return new DigitalDeviceModel
            {
                Name = precise.Name, Image = ImageToBitmap.ToBitmap(precise.Image?.ImageData!),
                AttributesModels =
                    new ObservableCollection<DigitalAttributesModel>(precise.Ranges
                        .Select(DigitalAtributesToModel.ToModel).ToList())
            };
        return new DigitalDeviceModel();
    }
}