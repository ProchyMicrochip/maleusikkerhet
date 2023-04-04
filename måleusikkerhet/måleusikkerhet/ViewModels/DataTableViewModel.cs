using System.Collections.ObjectModel;
using System.Linq;
using DynamicData;
using måleusikkerhet.Bases;
using måleusikkerhet.Infrastructure;
using måleusikkerhet.Models;
using måleusikkerhet.Services;
using ReactiveUI;
using Splat;

namespace måleusikkerhet.ViewModels;

public class DataTableViewModel : ViewModelBase
{
    private readonly ActiveDeviceService _activeDevicesService;
    private DevBase? _selection;
    private MeasurementType? _measurement;
    private ObservableCollection<DataModel> _data;
    private ObservableCollection<MeasurementType> _availableMeasurements;
    private decimal _k;

    public DataTableViewModel()
    {
        Name = "Table";
        _activeDevicesService = Locator.Current.GetService<ActiveDeviceService>();
        _data = new ObservableCollection<DataModel>();
        _selection = _activeDevicesService.DevBases.FirstOrDefault();
        _availableMeasurements = new ObservableCollection<MeasurementType>();
    }
    public string Name { get; }
    public ObservableCollection<DevBase> AvailableDevices => _activeDevicesService.DevBases;

    public ObservableCollection<MeasurementType> AvailableMeasurements
    {
        get => _availableMeasurements;
        set => this.RaiseAndSetIfChanged(ref _availableMeasurements, value);
    }

    public ObservableCollection<DataModel> Data
    {
        get => _data;
        set => this.RaiseAndSetIfChanged(ref _data, value);
    }

    public DevBase? Selection
    {
        get => _selection;
        set
        {
            AvailableMeasurements = value?.GetAvailableTypes() ?? new ObservableCollection<MeasurementType>();
            this.RaiseAndSetIfChanged(ref _selection, value);
        }
    }

    public MeasurementType? Measurement
    {
        get => _measurement;
        set
        {
            if (Selection is not null && value is not null)
            {
                if(Data.Count == 0) Data.AddRange(Enumerable.Repeat(new DataModel(Selection,(MeasurementType)value),10));
                Data = new ObservableCollection<DataModel>(Data.Select(x =>
                    new DataModel(Selection,(MeasurementType)value)));
            }
            this.RaiseAndSetIfChanged(ref _measurement, value);
        }
    }

    public decimal K
    {
        get => _k;
        set => this.RaiseAndSetIfChanged(ref _k, value);
    }
}