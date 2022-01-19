using NmeaParser.ViewModels;
using System.Windows;

namespace NmeaParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
            this.DataContext = DataContext;
        }

        public string Text { get; internal set; }
        public string Header { get; internal set; }
    }
}