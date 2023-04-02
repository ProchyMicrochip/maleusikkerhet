using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Media.Imaging;
using måleusikkerhet.Bases;
using måleusikkerhet.Database;
using måleusikkerhet.Views;
using Microsoft.EntityFrameworkCore;

namespace måleusikkerhet.Services;

public class DatabaseService
{
    private readonly DeviceDb _db;
    

    public IEnumerable<DeviceCardView> Devices => _db.AnalogDev.Include(x => x.Image).Select(x => x as DevBase).ToList()
        .Concat(_db.DigitalDev.Include(x => x.Image).Select(x => x as DevBase).ToList())
        .Concat(_db.PreciseDev.Include(x => x.Image).Select(x => x as DevBase)).OrderBy(x => x.Name).Select(x =>
        {
            using var ms = new MemoryStream();
            ms.Write(x.Image?.ImageData);
            ms.Position = 0;
            var bitmap = Bitmap.DecodeToHeight(ms, 128);
            return new DeviceCardView(x.Name, bitmap);
        });

    public DatabaseService()
    {
        _db = new DeviceDb();
    }

    public void AddDevice<T>(T device) where T : DevBase
    {
        switch (device)
        {
            case Analog analog:
                _db.AnalogDev.Add(analog);
                break;
            case Digital digital:
                _db.DigitalDev.Add(digital);
                break;
            case Precise precise:
                _db.PreciseDev.Add(precise);
                break;
            default:
                return;
        }
        _db.SaveChanges();
    }
}