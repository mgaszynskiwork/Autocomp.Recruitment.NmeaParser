using System.Windows;
using System.Windows.Controls;

namespace NmeaParser.Views
{
    /// <summary>
    /// Interaction logic for MessageView.xaml
    /// </summary>
    public partial class MessageView : UserControl
    {
        public MessageView()
        {
            InitializeComponent();
            TextToParse.Text = ((MainWindow)Application.Current.MainWindow).Text;
        }

        private void TextToParse_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Text = TextToParse.Text;
        }
    }
}