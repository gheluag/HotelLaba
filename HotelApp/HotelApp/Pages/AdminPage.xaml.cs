using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        DataBase db = new DataBase();
        public AdminPage()
        {
            InitializeComponent();
            LoadAdmins();
        }

        private void LoadAdmins()
        {
            var adminlists = db.GetAdmins();
            adminLB.ItemsSource = adminlists;
        }

        private void addRoomer_Click(object sender, RoutedEventArgs e)
        {
            AddAdmin addAdminWindow = new AddAdmin();

            if (addAdminWindow.ShowDialog() == true)
            {
                LoadAdmins();
            }
        }

        private void delRoomer_Click(object sender, RoutedEventArgs e)
        {
            DeleteAdmin deleteAdmin = new DeleteAdmin();
            deleteAdmin.ShowDialog();
        }
    }
}
