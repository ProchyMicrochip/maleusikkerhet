using Splat;

namespace måleusikkerhet.Services;

public static class ServiceRegistration
{
    public static void Register(IMutableDependencyResolver locator)
    {
        locator.RegisterConstant(new DatabaseService());
        locator.RegisterConstant(new CurrentDeviceService());
        locator.RegisterConstant(new ActiveDeviceService());
    }
}