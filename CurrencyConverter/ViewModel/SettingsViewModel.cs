using CommonServiceLocator;
using CurrencyConverter.Model;
using CurrencyConverter.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace CurrencyConverter.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ICurrencyService currencyService;

        #region fields
        private ObservableCollection<Currency> currencies;
        private ObservableCollection<Currency> availableCurrencies;
        private Currency selectedCurrency;
        private bool isSaveEnabled;
        private string selectedCode;
        private string currencyNameEdit;
        #endregion

        #region constructor
        public SettingsViewModel()
        {
            IsSaveEnabled = false;
            SaveSettingsCommand = new RelayCommand(SaveSettings);
            CancelCommand = new RelayCommand(Cancel);
            RemoveCurrencyCommand = new RelayCommand(RemoveCurrency);
            currencyService = ServiceLocator.Current.GetInstance<ICurrencyService>();

            GetCurrencies();
        }

        #endregion

        #region property
        public ObservableCollection<Currency> AvailableCurrencies
        {
            get => availableCurrencies;
            set
            {
                availableCurrencies = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Currency> Currencies
        {
            get => currencies;
            set
            {
                currencies = value;
                RaisePropertyChanged();
            }
        }

        public Currency SelectedCurrency
        {
            get => selectedCurrency;
            set
            {
                selectedCurrency = value;
                RaisePropertyChanged();
                IsSaveEnabled = SelectedCurrency != null;
            }
        }

        public bool IsSaveEnabled
        {
            get => isSaveEnabled;
            set
            {
                isSaveEnabled = value;
                RaisePropertyChanged();
            }
        }

        public string SelectedCode
        {
            get => selectedCode;
            set
            {
                selectedCode = value;
                RaisePropertyChanged();
            }
        }

        public string CurrencyNameEdit
        {
            get => currencyNameEdit;
            set
            {
                currencyNameEdit = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region public
        public ICommand SaveSettingsCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand RemoveCurrencyCommand { get; set; }

        public void GetCurrencies()
        {
            Currencies = new ObservableCollection<Currency>(currencyService.GetLocalCurrencies());
        }
        #endregion

        #region private
        /// <summary>
        /// Saves settings into json file.
        /// </summary>
        private void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(Currencies);
            using (StreamWriter str = new StreamWriter("Resources/settings.json"))
            {
                str.Write(json);
            }

            ViewModelLocator.Conversion.RefreshCurrencies();
            Cancel();
        }

        /// <summary>
        /// Closes Settings window
        /// </summary>
        private void Cancel()
        {
            Messenger.Default.Send(string.Empty, "CloseSettings");
        }

        /// <summary>
        /// Removes selected currency from list of saved currencies.
        /// </summary>
        private void RemoveCurrency()
        {
            if(SelectedCurrency != null)
                Currencies.Remove(SelectedCurrency);

            SelectedCurrency = null;
            IsSaveEnabled = true;
        }
        #endregion
    }
}
