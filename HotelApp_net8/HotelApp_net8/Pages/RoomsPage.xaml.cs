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

namespace HotelApp_net8.Pages
{
    /// <summary>
    /// Логика взаимодействия для RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        DataBase db = new DataBase();
        public RoomsPage()
        {
            InitializeComponent();
            LoadRooms();
        }

        private void LoadRooms()
        {
            var roomList = db.GetRooms();

            RoomsLb.ItemsSource = roomList;
        }

    }
}
