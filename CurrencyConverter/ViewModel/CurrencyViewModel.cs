using CommonServiceLocator;
using CurrencyConverter.Model;
using CurrencyConverter.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CurrencyConverter.ViewModel
{
    public class CurrencyViewModel : ViewModelBase
    {
        private readonly ICurrencyService currencyService;

        #region fields
        private ObservableCollection<Currency> currencies;
        private Currency currency;
        #endregion

        #region constructor
        public CurrencyViewModel()
        {
            SaveCommand = new RelayCommand(SaveCurrency);
            CancelCommand = new RelayCommand(Close);

            currencyService = ServiceLocator.Current.GetInstance<ICurrencyService>();

            SetCurrencies();
        }

        #endregion

        #region property
        public ObservableCollection<Currency> Currencies
        {
            get => currencies;
            set
            {
                currencies = value;
                RaisePropertyChanged();
            }
        }

        public Currency Currency
        {
            get => currency;
            set
            {
                currency = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region public
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        #endregion

        #region private
        /// <summary>
        /// Adds currencies in Settings page.
        /// </summary>
        private void SaveCurrency()
        {
            ViewModelLocator.Settings.Currencies.Add(Currency);
            ViewModelLocator.Settings.IsSaveEnabled = true;
            Close();
        }

        /// <summary>
        /// Closes window.
        /// </summary>
        private void Close()
        {
            Messenger.Default.Send(string.Empty, "CloseCurrencyWindow");
        }

        /// <summary>
        /// Sets currencies available for adding.
        /// </summary>
        private void SetCurrencies()
        {
            var allCurrencies = currencyService.GetCurrencies().Result;
            var existingCurrencies = ViewModelLocator.Settings.Currencies.ToList();

            var result = allCurrencies
               .Where(x => !existingCurrencies.Any(y => y.CurrencyCode == x.CurrencyCode)).ToList();

            Currencies = new ObservableCollection<Currency>(result);
        }
        #endregion
    }
}
