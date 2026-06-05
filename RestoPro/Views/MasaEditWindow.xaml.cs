using RestoPro.Data;
using RestoPro.Models;
using System.Windows;

namespace RestoPro.Views
{
    public partial class MasaEditWindow : Window
    {
        private Masa _existing;

        public MasaEditWindow(Masa existing = null)
        {
            InitializeComponent();
            _existing = existing;
            if (existing != null)
            {
                TitleText.Text = "Modifică Masă";
                TxtNumar.Text = existing.NumarMasa.ToString();
                TxtCapacitate.Text = existing.Capacitate.ToString();
                CbZona.Text = existing.Zona;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNumar.Text) ||
                string.IsNullOrWhiteSpace(TxtCapacitate.Text) ||
                string.IsNullOrWhiteSpace(CbZona.Text))
            {
                MessageBox.Show("Completați toate câmpurile.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(TxtNumar.Text, out int numar) ||
                numar <= 0)
            {
                MessageBox.Show("Numărul mesei trebuie să fie > 0.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(TxtCapacitate.Text, out int cap) ||
                cap <= 0)
            {
                MessageBox.Show("Capacitatea trebuie să fie > 0.",
                    "Validare", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var masa = new Masa
            {
                NumarMasa = numar,
                Capacitate = cap,
                Zona = CbZona.Text
            };

            if (_existing == null)
                DatabaseHelper.AddMasa(masa);
            else
            {
                masa.IdMasa = _existing.IdMasa;
                DatabaseHelper.UpdateMasa(masa);
            }

            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
            => DialogResult = false;
    }
}