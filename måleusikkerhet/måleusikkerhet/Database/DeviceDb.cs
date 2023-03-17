using System;
using Avalonia.Controls.Shapes;
using måleusikkerhet.Bases;
using Microsoft.EntityFrameworkCore;

namespace måleusikkerhet.Database;

public class DeviceDb: DbContext
{
    //Master password: pgAdmin
    public string DbPath { get; }
    public DbSet<Analog> AnalogDev { get; set; }
    public DbSet<Digital> DigitalDev { get; set; }
    public DbSet<Precise> PreciseDev { get; set; }

    public DeviceDb()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        path = System.IO.Path.Join(path, "måleusikkerhet");
        DbPath = System.IO.Path.Join(path, "devices.db");
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //    => options.UseNpgsql("Host=localhost;Database=Devices;Port=5432;Username=postgres;Password=pgAdmin");
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}