using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Media.Imaging;
using måleusikkerhet.Bases;
using måleusikkerhet.Database;
using måleusikkerhet.Models;
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
                var aRemove = _db.AnalogDev.FirstOrDefault(x => x.Name.Equals(analog.Name));
                if (aRemove is not null)
                    _db.AnalogDev.Remove(aRemove);
                _db.AnalogDev.Add(analog);
                break;
            case Digital digital:
                var dRemove = _db.DigitalDev.FirstOrDefault(x => x.Name.Equals(digital.Name));
                if (dRemove is not null)
                    _db.DigitalDev.Remove(dRemove);
                _db.DigitalDev.Add(digital);
                break;
            case Precise precise:
                var pRemove = _db.PreciseDev.FirstOrDefault(x => x.Name.Equals(precise.Name));
                if (pRemove is not null)
                    _db.PreciseDev.Remove(pRemove);
                _db.PreciseDev.Add(precise);
                break;
            default:
                return;
        }
        _db.SaveChanges();
    }

    public ModelBase GetDeviceModel(string? name)
    {
        try
        {
            return _db.PreciseDev.Include(x => x.Image)
                .Include(x => x.Ranges)
                .First(x => x.Name.Equals(name))
                .ToModel();
        }
        catch (Exception)
        {
            try
            {
                return _db.DigitalDev.Include(x => x.Image)
                    .Include(x => x.Ranges)
                    .First(x => x.Name.Equals(name))
                    .ToModel();
            }
            catch (Exception)
            {
                return _db.AnalogDev.Include(x => x.Image)
                    .Include(x => x.Ranges)
                    .First(x => x.Name.Equals(name))
                    .ToModel();
            }
        }
    }
    public DevBase GetDeviceBase(string? name)
    {
        try
        {
            return _db.PreciseDev
                .Include(x => x.Ranges)
                .First(x => x.Name.Equals(name));
        }
        catch (Exception)
        {
            try
            {
                return _db.DigitalDev
                    .Include(x => x.Ranges)
                    .First(x => x.Name.Equals(name));
            }
            catch (Exception)
            {
                return _db.AnalogDev
                    .Include(x => x.Ranges)
                    .First(x => x.Name.Equals(name));
            }
        }
    }
}