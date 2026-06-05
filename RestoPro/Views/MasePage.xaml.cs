using RestoPro.Data;
using System.Windows.Controls;

namespace RestoPro.Views
{
    public partial class MasePage : Page
    {
        public MasePage()
        {
            InitializeComponent();
            DgMese.ItemsSource = DatabaseHelper.GetAllMese();
        }
    }
}