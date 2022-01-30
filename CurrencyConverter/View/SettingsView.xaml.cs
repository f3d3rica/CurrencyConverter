using CurrencyConverter.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace CurrencyConverter.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Settings;
            Messenger.Default.Register<string>(this, "CloseSettings", x =>
            {
                    this.Close();
            });
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newCurrency = new CurrencyView { Owner = GetWindow(this) };
            newCurrency.ShowDialog();
            newCurrency.Focus();
        }
    }
}
