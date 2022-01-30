using CommonServiceLocator;
using CurrencyConverter.Model;
using CurrencyConverter.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CurrencyConverter.ViewModel
{
    public class ConversionViewModel : ViewModelBase
    {
        #region fields
        private readonly ICurrencyService currencyService;
        private double amount;
        private ObservableCollection<Currency> currenciesFrom;
        private ObservableCollection<Currency> currenciesTo;
        private Currency currencyFrom;
        private Currency currencyTo;
        private double result;
        #endregion

        #region constructor
        public ConversionViewModel()
        {
            currencyService = ServiceLocator.Current.GetInstance<ICurrencyService>();
            SwapCommand = new RelayCommand(SwapCurrencies);
            ShowSettingsCommand = new RelayCommand(ShowSettings);

            RefreshCurrencies();
        }
        #endregion

        #region property
        public Currency CurrencyFrom
        {
            get => currencyFrom;
            set
            {
                currencyFrom = value;
                RaisePropertyChanged();
                ConvertRate();
            }
        }

        public Currency CurrencyTo
        {
            get => currencyTo;
            set
            {
                currencyTo = value;
                RaisePropertyChanged();
                ConvertRate();
            }
        }

        public double Amount
        {
            get => amount;
            set
            {
                amount = value;
                RaisePropertyChanged();
                ConvertRate();
            }
        }

        public double Result
        {
            get => result;
            set
            {
                result = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Currency> CurrenciesFrom
        {
            get => currenciesFrom;
            set
            {
                currenciesFrom = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Currency> CurrenciesTo
        {
            get => currenciesTo;
            set
            {
                currenciesTo = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region public
        public ICommand SwapCommand { get; set; }
        public ICommand ShowSettingsCommand { get; set; }

        public void RefreshCurrencies()
        {
            var data = currencyService.GetLocalCurrencies().ToList();
            CurrenciesFrom = new ObservableCollection<Currency>(data);
            CurrenciesTo = new ObservableCollection<Currency>(data);

            Amount = 0;
            Result = 0;
        }

        #endregion

        #region private

        private void ConvertRate()
        {
            if (CurrencyFrom == null || CurrencyTo == null) return;

            var rate = currencyService.GetLatestCurrencyRate(CurrencyFrom, CurrencyTo).Result;
            Result = Amount * Convert.ToDouble(rate);
        }

        private void SwapCurrencies()
        {
            var tempCurrencyFrom = currencyFrom;
            CurrencyFrom = CurrencyTo;
            CurrencyTo = tempCurrencyFrom;
        }

        private void ShowSettings()
        {
            Messenger.Default.Send(string.Empty, "ShowSettingsWindow");
            if(ViewModelLocator.Settings != null)
            {
                ViewModelLocator.Settings.GetCurrencies();
                ViewModelLocator.Settings.IsSaveEnabled = false;

            }
        }

        #endregion
    }
}
