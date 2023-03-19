using måleusikkerhet.Infrastructure;
using ReactiveUI;

namespace måleusikkerhet.ViewModels;

public class AttributeListWindowModel : ViewModelBase
{
    private PreciseSmartCollection _smartCollection = new();

    public PreciseSmartCollection SmartCollection
    {
        get => _smartCollection;
        set => this.RaiseAndSetIfChanged(ref _smartCollection, value);
    }

    public AttributeListWindowModel()
    {
        SmartCollection.Add(new PreciseAttributesModel());
    }
}