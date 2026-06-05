using Microsoft.Win32;
using RestoPro.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace RestoPro.Views
{
    public partial class RaportWindow : Window
    {
        public RaportWindow()
        {
            InitializeComponent();
            LoadReport();
        }

        private void LoadReport()
        {
            var data = DatabaseHelper.GetRaportMese();
            DgRaport.ItemsSource = data;

            int totalComenzi = data.Sum(x => x.NrComenzi);
            decimal totalIncasat = data.Sum(x => x.TotalAchitat);
            decimal media = data.Count > 0
                ? totalIncasat / data.Count : 0;
            string topProdus =
                DatabaseHelper.GetProdusCelMaiVandut();

            TxtTotalComenzi.Text =
                $"Total comenzi: {totalComenzi}";
            TxtTotalIncasat.Text =
                $"Total încasat: {totalIncasat:F2} lei";
            TxtMediaMasa.Text =
                $"Medie per masă: {media:F2} lei";
            TxtProdusCelMaiVandut.Text =
                $"Produsul cel mai vândut: {topProdus}";
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                FileName = "RaportRestoPro",
                DefaultExt = ".txt",
                Filter = "Text files (*.txt)|*.txt"
            };
            if (dlg.ShowDialog() != true) return;

            var data = DatabaseHelper.GetRaportMese();
            using (var sw = new StreamWriter(dlg.FileName))
            {
                sw.WriteLine("RAPORT SUMAR — RestoPro");
                sw.WriteLine(new string('=', 50));
                sw.WriteLine($"{"Nr.Masă",-10} {"Zonă",-15}" +
                    $" {"Nr.Comenzi",-12} {"Total Achitat",-15}");
                sw.WriteLine(new string('-', 50));
                foreach (var r in data)
                    sw.WriteLine($"{r.NumarMasa,-10} {r.Zona,-15}" +
                        $" {r.NrComenzi,-12} {r.TotalAchitat:F2} lei");
                sw.WriteLine(new string('=', 50));
                sw.WriteLine($"Total comenzi : " +
                    $"{data.Sum(x => x.NrComenzi)}");
                sw.WriteLine($"Total încasat : " +
                    $"{data.Sum(x => x.TotalAchitat):F2} lei");
                sw.WriteLine($"Produs top    : " +
                    $"{DatabaseHelper.GetProdusCelMaiVandut()}");
            }

            MessageBox.Show("Raportul a fost exportat cu succes!",
                "Export", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
            => Close();
    }
}