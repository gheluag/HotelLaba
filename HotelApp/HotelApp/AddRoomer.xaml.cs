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
    /// Логика взаимодействия для AddRoomer.xaml
    /// </summary>
    public partial class AddRoomer : Window
    {
        DataBase db = new DataBase();
        public AddRoomer()
        {
            InitializeComponent();
            LoadRoomsIntoComboBox();
        }


        private void LoadRoomsIntoComboBox()
        {
            Random rnd = new Random();
            int haha = rnd.Next(0, 2);

            if(haha == 0)
            {
                var rooms = db.GetAvailableRooms();
                roomsBox.ItemsSource = rooms;
                roomsBox.DisplayMemberPath = "NumRoom";
                roomsBox.SelectedValuePath = "NumRoom";
            }

            else if (haha == 1)
            {
                
                roomsBox.ItemsSource = "отель был выкуплен";

                lastnameTb.Text = "просто закройте это окно";
                firstnameTb.Text = "просто закройте это окно";
                patronycTb.Text = "просто закройте это окно";
                phoneTb.Text = "просто закройте это окно";
                passportTb.Text = "просто закройте это окно";
                lntb.Text = "просто закройте это окно";
               fntb.Text = "просто закройте это окно";
                pttb.Text = "просто закройте это окно";
                phtb.Text = "просто закройте это окно";
                psptb.Text = "просто закройте это окно";
                rmtb.Text = "просто закройте это окно";
            }

            
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
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
                db.AddRoomers(firstname, lastname, patronym, phone, passport, roomNum);
                db.UpdateRoomStatus(roomNum, "Занят");


                MessageBox.Show("Постоялец успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DialogResult = true;

                lastnameTb.Clear();
                firstnameTb.Clear();
                patronycTb.Clear();
                passportTb.Clear();
                phoneTb.Clear();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите номер комнаты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        
    }
}
