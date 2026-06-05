using RestoPro.Data;
using RestoPro.Models;
using System.Windows;

namespace RestoPro.Views
{
    public partial class ProdusEditWindow : Window
    {
        private Produs _existing;

        public ProdusEditWindow(Produs existing = null)
        {
            InitializeComponent();
            _existing = existing;
            if (existing != null)
            {
                TitleText.Text = "Modifică Produs";
                TxtDenumire.Text = existing.Denumire;
                CbCategorie.Text = existing.Categorie;
                TxtPret.Text = existing.Pret.ToString("F2");
                ChkDisponibil.IsChecked = existing.Disponibil;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtDenumire.Text) ||
                string.IsNullOrWhiteSpace(CbCategorie.Text) ||
                string.IsNullOrWhiteSpace(TxtPret.Text))
            {
                MessageBox.Show("Completați toate câmpurile.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            if (!decimal.TryParse(TxtPret.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal pret) || pret <= 0)
            {
                MessageBox.Show("Prețul trebuie să fie > 0.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var p = new Produs
            {
                Denumire = TxtDenumire.Text.Trim(),
                Categorie = CbCategorie.Text,
                Pret = pret,
                Disponibil = ChkDisponibil.IsChecked == true
            };

            if (_existing == null)
                DatabaseHelper.AddProdus(p);
            else
            {
                p.IdProdus = _existing.IdProdus;
                DatabaseHelper.UpdateProdus(p);
            }
            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
            => DialogResult = false;
    }
}