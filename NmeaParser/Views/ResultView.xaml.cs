using System.Windows;
using System.Windows.Controls;
using NmeaParser.ViewModels;

namespace NmeaParser.Views
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : UserControl
    {
        public ResultViewModel ViewModel;

        public ResultView()
        {
            InitializeComponent();

            DataContext = ViewModel = new ResultViewModel(this);
            this.DataContext = DataContext;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Header = (e.AddedItems[0] as ComboBoxItem).Content as string;
            if (ViewModel != null)
            {
                ViewModel.SelectionChanged();
            }
        }
    }
}