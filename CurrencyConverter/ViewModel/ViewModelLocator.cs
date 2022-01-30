using GalaSoft.MvvmLight.Ioc;

namespace CurrencyConverter.ViewModel
{
    public static class ViewModelLocator
    {
        public static ConversionViewModel Conversion
        {
            get
            {
                if (!SimpleIoc.Default.IsRegistered<ConversionViewModel>())
                {
                    SimpleIoc.Default.Register<ConversionViewModel>();
                }

                return SimpleIoc.Default.GetInstance<ConversionViewModel>();
            }
        }

        public static SettingsViewModel Settings
        {
            get
            {
                if (!SimpleIoc.Default.IsRegistered<SettingsViewModel>())
                {
                    SimpleIoc.Default.Register<SettingsViewModel>();
                }

                return SimpleIoc.Default.GetInstance<SettingsViewModel>();
            }
        }
    }
}
