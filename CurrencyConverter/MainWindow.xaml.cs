using CurrencyConverter.View;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, "ShowSettingsWindow", x =>
            {
                var settings = new SettingsView{ Owner = GetWindow(this) };
                settings.ShowDialog();
                settings.Focus();
            });
        }
    }
}
