using CurrencyConverter.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace CurrencyConverter.View
{
    /// <summary>
    /// Interaction logic for CurrencyView.xaml
    /// </summary>
    public partial class CurrencyView : Window
    {
        public CurrencyView()
        {
            InitializeComponent();
            DataContext = new CurrencyViewModel();
            Messenger.Default.Register<string>(this, "CloseCurrencyWindow", x =>
            {
                this.Close();
            });
        }
    }
}
