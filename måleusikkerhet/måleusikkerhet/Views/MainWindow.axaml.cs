using Avalonia.Controls;
using Avalonia.Interactivity;
using måleusikkerhet.ViewModels;

namespace måleusikkerhet.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _model = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _model;
        }
    }
}