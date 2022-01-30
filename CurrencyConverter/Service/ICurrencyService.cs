using CurrencyConverter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConverter.Service
{
    public interface ICurrencyService
    {
        /// <summary>
        /// Gets list of available currency codes and names
        /// </summary>
        /// <returns>List of currencies.</returns>
        Task<IList<Currency>> GetCurrencies();

        /// <summary>
        /// Gets latest rate for selected currency pair.
        /// </summary>
        /// <param name="currencyFrom">Currency to convert from.</param>
        /// <param name="currencyTo">C=Destination currency</param>
        /// <returns>destination currency rate.</returns>
        Task<double> GetLatestCurrencyRate(Currency currencyFrom, Currency currencyTo);

        /// <summary>
        /// Gets list of available currency codes and names
        /// </summary>
        /// <returns>List of currencies.</returns>
        IList<Currency> GetLocalCurrencies();
    }
}
