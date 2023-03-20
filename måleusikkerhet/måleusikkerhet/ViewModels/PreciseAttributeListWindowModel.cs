using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Media;
using måleusikkerhet.Infrastructure;
using ReactiveUI;

namespace måleusikkerhet.ViewModels;

public class AttributeListWindowModel : ViewModelBase
{
    private ObservableCollection<PreciseAttributesModel> _smartCollection = new();

    public ObservableCollection<PreciseAttributesModel> SmartCollection
    {
        get => _smartCollection;
        set => this.RaiseAndSetIfChanged(ref _smartCollection, value);
    }

    public AttributeListWindowModel()
    {
        var model = new PreciseAttributesModel();
        model.PropertyChanged += ModelOnPropertyChanged;
        SmartCollection.Add(model);
    }
    public delegate void InvalidatedHandler();

    public event InvalidatedHandler Invalidate;

    private void ModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(sender as PreciseAttributesModel? is {RangeError: null, MeasureError: null, Range: null, Frequency: null})return;
        var result = false;
        switch (e.PropertyName)
        {
            case nameof(PreciseAttributesModel.Range):
                if (sender as PreciseAttributesModel? is { RangeError: null, Frequency: null, MeasureError: null })
                    result = true;
                break;
            case nameof(PreciseAttributesModel.RangeError):
                if (sender as PreciseAttributesModel? is { Range: null, Frequency: null, MeasureError: null })
                    result = true;
                break;
            case nameof(PreciseAttributesModel.Frequency):
                if (sender as PreciseAttributesModel? is { RangeError: null, Range: null, MeasureError: null })
                    result = true;
                break;
            case nameof(PreciseAttributesModel.MeasureError):
                if (sender as PreciseAttributesModel? is { RangeError: null, Frequency: null, Range: null })
                    result = true;
                break;
        }
        if (!result) return;
        var model = new PreciseAttributesModel();
        model.PropertyChanged += ModelOnPropertyChanged;
        SmartCollection.Add(model);
        Invalidate?.Invoke();
    }
}