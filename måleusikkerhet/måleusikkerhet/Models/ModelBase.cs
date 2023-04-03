using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;

namespace måleusikkerhet.Models;

public abstract class ModelBase: INotifyPropertyChanged
{
    private string _name = string.Empty;
    private Bitmap? _image;
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public Bitmap? Image
    {
        get => _image;
        set => SetField(ref _image, value);
    }
    #region PropertyChanged
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    #endregion
}