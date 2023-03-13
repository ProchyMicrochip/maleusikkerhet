using måleusikkerhet.Bases;
using Microsoft.EntityFrameworkCore;

namespace måleusikkerhet.Database;

public class DeviceDb: DbContext
{
    //Master password: pgAdmin
    public DbSet<Analog> AnalogDev { get; set; }
    public DbSet<Digital> DigitalDev { get; set; }
    public DbSet<Precise> PreciseDev { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=localhost;Database=Devices;Port=5432;Username=postgres;Password=pgAdmin");
    
}