﻿using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using måleusikkerhet.ViewModels;

namespace måleusikkerhet.Views;

public partial class AddDeviceForm : UserControl
{
    private readonly AddDeviceFormViewModel _model = new();
    public AddDeviceForm()
    {
        InitializeComponent();
        DataContext = _model;
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void LoadImage(object? sender, RoutedEventArgs e)
    {
        _model.LoadImage();
    }
}