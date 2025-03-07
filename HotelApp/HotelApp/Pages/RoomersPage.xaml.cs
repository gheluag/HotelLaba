using HotelApp.TablesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Логика взаимодействия для RoomersPage.xaml
    /// </summary>
    public partial class RoomersPage : Page
    {
        DataBase db = new DataBase();
        public RoomersPage()
        {
            InitializeComponent();
            LoadRoomers();
        }

        private void LoadRoomers()
        {
            var roomerlst = db.GetRoomers();
            roomersLB.ItemsSource = roomerlst;
        }

        private void findBtn_Click(object sender, RoutedEventArgs e)
        {
            string searchText = "УЧИТЬ C#";

            int numberOfItems = roomersLB.Items.Count;

            var newRoomersList = new List<TablesModels.Roomers>();

            for (int i = 0; i < numberOfItems; i++)
            {
                newRoomersList.Add(new TablesModels.Roomers
                {
                    RoomNum = 1000011, 
                    Surname = searchText,
                    FirstName = searchText,
                    Patronymic = searchText,
                    Passport = searchText,
                    Phone = searchText
                });
            }
            searchBox.Clear();

            roomersLB.ItemsSource = newRoomersList;

            Madness madness = new Madness();
            madness.ShowDialog();

        }

        private void addRoomer_Click(object sender, RoutedEventArgs e)
        {
            AddRoomer addRoomer = new AddRoomer();
            addRoomer.ShowDialog();
            if(addRoomer.DialogResult == true)
            {
                LoadRoomers();
            }
        }

        private void delRoomer_Click(object sender, RoutedEventArgs e)
        {
            if (roomersLB.SelectedItem is Roomers selectedRoomer)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выселить этого постояльца?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    db.CheckOutRoomer(selectedRoomer.IdRoomer);
                    LoadRoomers();
                }

                MessageBox.Show("Постоялец успешно выселен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите постояльца для выселения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (roomersLB.SelectedItem is Roomers selectedRoomer)
            {
                EditRoomer editRoomer = new EditRoomer(selectedRoomer);

                if (editRoomer.ShowDialog() == true)
                {
                    LoadRoomers();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите постояльца для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
