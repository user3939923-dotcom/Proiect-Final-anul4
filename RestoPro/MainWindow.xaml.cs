using RestoPro.Data;
using RestoPro.Views;
using System.Windows;

namespace RestoPro
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!DatabaseHelper.TestConnection())
            {
                MessageBox.Show("Nu s-a putut conecta la baza de date.",
                    "Eroare critică", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }
            MainFrame.Navigate(new MasePage());
        }

        private void BtnMese_Click(object sender, RoutedEventArgs e)
            => MainFrame.Navigate(new MasePage());

        private void BtnProduse_Click(object sender, RoutedEventArgs e)
            => MainFrame.Navigate(new ProdusePage());

        private void BtnComenzi_Click(object sender, RoutedEventArgs e)
            => MainFrame.Navigate(new ComenziPage());

        private void BtnRaport_Click(object sender, RoutedEventArgs e)
        {
            var raport = new RaportWindow();
            raport.ShowDialog();
        }
    }
}