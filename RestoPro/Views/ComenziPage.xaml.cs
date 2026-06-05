using RestoPro.Data;
using RestoPro.Models;
using System.Windows;
using System.Windows.Controls;

namespace RestoPro.Views
{
    public partial class ComenziPage : Page
    {
        public ComenziPage()
        {
            InitializeComponent();
            LoadFilters();
            LoadData();
        }

        private void LoadFilters()
        {
            CbFilterMasa.Items.Clear();
            CbFilterMasa.Items.Add("Toate");
            foreach (var m in DatabaseHelper.GetAllMese())
                CbFilterMasa.Items.Add($"Masa {m.NumarMasa}");
            CbFilterMasa.SelectedIndex = 0;
            CbFilterStatus.SelectedIndex = 0;
        }

        private void LoadData()
        {
            string masaItem =
                CbFilterMasa.SelectedItem?.ToString();
            string statusItem =
                CbFilterStatus.SelectedItem is ComboBoxItem cbi
                ? cbi.Content.ToString() : "Toate";

            int? idMasa = null;
            if (masaItem != null && masaItem != "Toate")
            {
                var nr = int.Parse(masaItem.Replace("Masa ", ""));
                idMasa = DatabaseHelper.GetMasaIdByNr(nr);
            }

            DgComenzi.ItemsSource =
                DatabaseHelper.FilterComenzi(
                    idMasa,
                    statusItem == "Toate" ? null : statusItem);
        }

        

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new ComandaAddWindow();
            if (win.ShowDialog() == true)
            {
                LoadFilters();
                LoadData();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgComenzi.SelectedItem is Comanda sel)
            {
                var res = MessageBox.Show(
                    $"Anulezi comanda #{sel.IdComanda}?",
                    "Confirmare", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (res == MessageBoxResult.Yes)
                {
                    DatabaseHelper.DeleteComanda(sel.IdComanda);
                    LoadData();
                }
            }
        }
    }
}