using CurrencyConverter.ViewModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CurrencyConverter.View
{
    /// <summary>
    /// Interaction logic for ConversionView.xaml
    /// </summary>
    public partial class ConversionView : UserControl
    {
        public ConversionView()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Conversion;
        }

        private void ValidateAmountInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
