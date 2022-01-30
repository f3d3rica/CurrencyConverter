using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Model
{
    public class Currency : INotifyPropertyChanged
    {
        private string currencyCode;
        private string currencyName;
        public Currency()
        {

        }

        public string FullName => string.Format("{0} - {1}", CurrencyCode, CurrencyName);
        public string CurrencyCode
        {
            get => currencyCode;
            set
            {
                currencyCode = value;
                OnPropertyChanged(nameof(CurrencyCode));
            }
        }

        public string CurrencyName
        {
            get => currencyName;
            set
            {
                currencyName = value;
                OnPropertyChanged(nameof(CurrencyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
