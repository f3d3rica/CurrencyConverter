using CurrencyConverter.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyConverter.Service
{
    public class CurrencyService : ICurrencyService
    {
        /// <summary>
        /// Gets list of available currency codes and names
        /// </summary>
        /// <returns>List of currencies.</returns>
        public async Task<IList<Currency>> GetCurrencies()
        {
            var result = new List<Currency>();
            try
            {
                using (var client = new HttpClient())
                {
                    var response =
                    client.GetAsync(
                       "https://cdn.jsdelivr.net/gh/fawazahmed0/currency-api@1/latest/currencies.json").Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var currencies = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                        if (result != null)
                        {
                            foreach (var currency in currencies)
                                result.Add(new Currency
                                {
                                    CurrencyCode = currency.Key.ToUpper(),
                                    CurrencyName = currency.Value
                                });
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Gets latest rate for selected currency pair.
        /// </summary>
        /// <param name="currencyFrom">Currency to convert from.</param>
        /// <param name="currencyTo">C=Destination currency</param>
        /// <returns>destination currency rate.</returns>
        public async Task<double> GetLatestCurrencyRate(Currency currencyFrom, Currency currencyTo)
        {
            double rate = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    var response =
                    client.GetAsync(
                         $"https://cdn.jsdelivr.net/gh/fawazahmed0/currency-api@1/latest/currencies/{currencyFrom.CurrencyCode.ToLower()}/{currencyTo.CurrencyCode.ToLower()}.json").Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var currencies = await response.Content.ReadAsStringAsync();
                        var jObject = JObject.Parse(currencies);
                        var currencyRate = jObject.GetValue(currencyTo.CurrencyCode.ToLower());
                        rate = Convert.ToDouble(currencyRate);
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return rate;
        }

        /// <summary>
        /// Gets list of localy saved currencies
        /// </summary>
        /// <returns>List of currencies.</returns>
        public IList<Currency> GetLocalCurrencies()
        {
            try
            {
                using (StreamReader r = new StreamReader("Resources/settings.json"))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<Currency>>(json);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new List<Currency>();
        }
    }
}
