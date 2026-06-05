using RestoPro.Data;
using RestoPro.Models;
using System.Windows;

namespace RestoPro.Views
{
    public partial class ComandaAddWindow : Window
    {
        public ComandaAddWindow()
        {
            InitializeComponent();
            CbMasa.ItemsSource = DatabaseHelper.GetAllMese();
            CbProdus.ItemsSource =
                DatabaseHelper.FilterProduse(null, null);
            CbStatus.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CbMasa.SelectedValue == null ||
                CbProdus.SelectedValue == null)
            {
                MessageBox.Show("Selectați masa și produsul.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(TxtCantitate.Text, out int cant)
                || cant <= 0)
            {
                MessageBox.Show("Cantitatea trebuie să fie > 0.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            int idMasa = (int)CbMasa.SelectedValue;
            int idProdus = (int)CbProdus.SelectedValue;

            if (DatabaseHelper.ComandaExista(idMasa, idProdus))
            {
                MessageBox.Show(
                    "Acest produs a fost deja comandat " +
                    "pentru această masă.",
                    "Duplicat", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var c = new Comanda
            {
                IdMasa = idMasa,
                IdProdus = idProdus,
                Cantitate = cant,
                DataComanda = System.DateTime.Today,
                StatusPlata = (CbStatus.SelectedItem
                    as System.Windows.Controls.ComboBoxItem)
                    ?.Content.ToString()
            };

            DatabaseHelper.AddComanda(c);
            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
            => DialogResult = false;
    }
}