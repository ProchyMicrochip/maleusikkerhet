using Avalonia.Web.Blazor;

namespace måleusikkerhet.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        WebAppBuilder.Configure<måleusikkerhet.App>()
            .SetupWithSingleViewLifetime();
    }
}