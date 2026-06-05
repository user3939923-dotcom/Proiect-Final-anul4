using RestoPro.Data;
using RestoPro.Models;
using System.Windows;
using System.Windows.Controls;

namespace RestoPro.Views
{
    public partial class MasePage : Page
    {
        public MasePage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData(string search = null)
        {
            DgMese.ItemsSource = search == null
                ? DatabaseHelper.GetAllMese()
                : DatabaseHelper.SearchMese(search);
        }

        private void DgMese_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            bool sel = DgMese.SelectedItem != null;
            BtnEdit.IsEnabled = sel;
            BtnDelete.IsEnabled = sel;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new MasaEditWindow();
            if (win.ShowDialog() == true) LoadData();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DgMese.SelectedItem is Masa selected)
            {
                var win = new MasaEditWindow(selected);
                if (win.ShowDialog() == true) LoadData();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgMese.SelectedItem is Masa selected)
            {
                var result = MessageBox.Show(
                    $"Ștergi masa nr. {selected.NumarMasa}?",
                    "Confirmare ștergere",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    DatabaseHelper.DeleteMasa(selected.IdMasa);
                    LoadData();
                }
            }
        }

        //Search_Button
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
            => LoadData(TxtSearch.Text.Trim());

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            TxtSearch.Clear();
            LoadData();
        }
    }
}