using System.Collections.ObjectModel;
using måleusikkerhet.Bases;

namespace måleusikkerhet.Services;

public class ActiveDeviceService
{
    public ObservableCollection<DevBase> DevBases { get; set; } = new();
}