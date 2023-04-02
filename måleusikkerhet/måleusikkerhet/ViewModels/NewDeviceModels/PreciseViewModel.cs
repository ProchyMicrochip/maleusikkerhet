using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;
using måleusikkerhet.Services;
using måleusikkerhet.Views;
using ReactiveUI;
using Splat;

namespace måleusikkerhet.ViewModels.NewDeviceModels;

public class PreciseViewModel : ViewModelBase
{

    
    public PreciseViewModel()
    {
       Edit = ReactiveCommand.CreateFromTask<IGrouping<MeasurementType,PreciseAttributesModel>>(RunTheThing);
    }



    public ReactiveCommand<IGrouping<MeasurementType,PreciseAttributesModel>, Unit> Edit { get; }

    private async Task RunTheThing(IGrouping<MeasurementType,PreciseAttributesModel> preciseAttributes)
    {

    }
    
}
