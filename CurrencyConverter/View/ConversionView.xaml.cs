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
            int n;
            bool isNumeric = int.TryParse(e.Text, out n);
            if (isNumeric)
            {
                e.Handled = !isNumeric;
            }
            else
            {
                if(e.Text.Equals(".") && !((TextBox)sender).Text.Contains("."))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            };
        }
    }
}
