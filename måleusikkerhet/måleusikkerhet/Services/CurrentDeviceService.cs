using System.Linq;
using måleusikkerhet.Bases;
using måleusikkerhet.Infrastructure;
using måleusikkerhet.Models;

namespace måleusikkerhet.Services;

public class CurrentDeviceService
{
    public PreciseDeviceModel? Model { get; set; }
    public CurrentDeviceService()
    {
        //dummy
    }
    
}