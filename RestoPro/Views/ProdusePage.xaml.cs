using RestoPro.Data;
using RestoPro.Models;
using System.Windows;
using System.Windows.Controls;

namespace RestoPro.Views
{
    public partial class ProdusePage : Page
    {
        public ProdusePage()
        {
            InitializeComponent();
            LoadCategories();
            LoadData();
        }

        private void LoadCategories()
        {
            CbFilterCategorie.Items.Clear();
            CbFilterCategorie.Items.Add("Toate");
            foreach (var cat in DatabaseHelper.GetCategorii())
                CbFilterCategorie.Items.Add(cat);
            CbFilterCategorie.SelectedIndex = 0;
        }

        private void LoadData()
        {
            string cat = CbFilterCategorie.SelectedItem?.ToString();
            string search = TxtSearch.Text.Trim();
            DgProduse.ItemsSource =
                DatabaseHelper.FilterProduse(
                    cat == "Toate" ? null : cat, search);
        }

        private void DgProduse_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            bool sel = DgProduse.SelectedItem != null;
            BtnEdit.IsEnabled = sel;
            BtnDelete.IsEnabled = sel;
        }
        private void CbFilter_Changed(object sender,
        SelectionChangedEventArgs e) => LoadData();

        private void BtnSearch_Click(object sender,
            RoutedEventArgs e) => LoadData();

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            TxtSearch.Clear();
            CbFilterCategorie.SelectedIndex = 0;
            LoadData();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProdusEditWindow();
            if (win.ShowDialog() == true) LoadData();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DgProduse.SelectedItem is Produs sel)
            {
                var win = new ProdusEditWindow(sel);
                if (win.ShowDialog() == true) LoadData();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgProduse.SelectedItem is Produs sel)
            {
                var res = MessageBox.Show(
                    $"Ștergi produsul '{sel.Denumire}'?",
                    "Confirmare", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (res == MessageBoxResult.Yes)
                {
                    if (!DatabaseHelper.CanDeleteProdus(sel.IdProdus))
                    {
                        MessageBox.Show(
                            "Produsul are comenzi asociate " +
                            "și nu poate fi șters.",
                            "Eroare", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }
                    DatabaseHelper.DeleteProdus(sel.IdProdus);
                    LoadData();
                }
            }
        }
    }
}