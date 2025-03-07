using HotelApp.TablesModels;
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
using System.Windows.Shapes;

namespace HotelApp
{
    /// <summary>
    /// Логика взаимодействия для EditRoomer.xaml
    /// </summary>
    public partial class EditRoomer : Window
    {
        private Roomers _roomer;
        private DataBase _db;
        public EditRoomer(Roomers roomer)
        {
            InitializeComponent();

            _roomer = roomer;
            _db = new DataBase();

            lastnameTb.Text = _roomer.Surname;
            firstnameTb.Text = _roomer.FirstName;
            patronycTb.Text = _roomer.Patronymic;
            passportTb.Text = _roomer.Passport;
            phoneTb.Text = _roomer.Phone;

            LoadAvailableRooms();
        }

        private void LoadAvailableRooms()
        {
            var availableRooms = _db.GetAvailableRooms();

            roomsBox.ItemsSource = availableRooms;
            roomsBox.DisplayMemberPath = "NumRoom";
            roomsBox.SelectedValuePath = "NumRoom";

            roomsBox.SelectedValue = _roomer.RoomNum;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string lastname = lastnameTb.Text.Trim();
            string firstname = firstnameTb.Text.Trim();
            string patronym = patronycTb.Text.Trim();
            string passport = passportTb.Text.Trim();
            string phone = phoneTb.Text.Trim();

            if (roomsBox.SelectedItem is Rooms selectedRoom)
            {
                int roomNum = selectedRoom.NumRoom;

                if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname) || string.IsNullOrEmpty(patronym)
                    || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(passport))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _db.UpdateRoomer(_roomer.IdRoomer, firstname, lastname, patronym, phone, passport, roomNum);

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите номер комнаты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
