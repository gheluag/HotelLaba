﻿using Org.BouncyCastle.Asn1.Cms;
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

namespace HotelApp_net8
{
    /// <summary>
    /// Логика взаимодействия для AddJournal.xaml
    /// </summary>
    public partial class AddJournal : Window
    {
        private DataBase _db;

        public AddJournal()
        {
            InitializeComponent();
            _db = new DataBase();

            LoadRoomers();
        }


        private void LoadRoomers()
        {
            var roomers = _db.GetRoomers();

            roomerBox.ItemsSource = roomers;

            roomerBox.DisplayMemberPath = "FullName";

            roomerBox.SelectedValuePath = "IdRoomer";
        }

        int roomNumber;
        private void roomerBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roomerBox.SelectedItem == null)
            {
                roomerroom.Text = string.Empty; 
                return;
            }

            int idRoomer = (int)roomerBox.SelectedValue;

            roomNumber = _db.GetRoomNumberByRoomerId(idRoomer);

            roomerroom.Text = roomNumber.ToString();
        }
        /*private void LoadRooms()
        {
            var rooms = _db.GetAvailableRooms();
            roomBox.ItemsSource = rooms;
            roomBox.DisplayMemberPath = "NumRoom";
            roomBox.SelectedValuePath = "NumRoom";
        }*/

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (roomerBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите постояльца.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int idRoomer = (int)roomerBox.SelectedValue;

           /* if (roomBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите номер комнаты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int roomNum = (int)roomBox.SelectedValue;*/

            if (entryDatePicker.SelectedDate == null || departureDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, укажите даты заезда и выезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateOnly entryDate = DateOnly.FromDateTime(entryDatePicker.SelectedDate.Value);
            DateOnly departureDate = DateOnly.FromDateTime(departureDatePicker.SelectedDate.Value);

            if (departureDate < entryDate)
            {
                MessageBox.Show("Дата выезда не может быть раньше даты заезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TimeOnly regTime = TimeOnly.FromDateTime(DateTime.Now);
            try
            {
                _db.AddJournalEntry(idRoomer, roomNumber, entryDate, departureDate, regTime);

                MessageBox.Show("Запись успешно добавлена в журнал!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
