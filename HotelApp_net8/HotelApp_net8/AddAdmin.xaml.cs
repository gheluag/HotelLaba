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
    /// Логика взаимодействия для AddAdmin.xaml
    /// </summary>
    public partial class AddAdmin : Window
    {
        DataBase db = new DataBase();
        public AddAdmin()
        {
            InitializeComponent();
            LoadJournalNumbers();
        }

        private void LoadJournalNumbers()
        {
            try
            {
                var journalIds = db.GetJournalIds();

                numjournal.ItemsSource = journalIds;
                numjournal.DisplayMemberPath = "Id";
                numjournal.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке номеров журналов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            string fioAdmin = fioBox.Text.Trim();

            if (numjournal.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите номер журнала.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int idJournal = (int)numjournal.SelectedValue;

            if (string.IsNullOrWhiteSpace(fioAdmin))
            {
                MessageBox.Show("Пожалуйста, заполните поле ФИО.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                db.AddAdmin(fioAdmin, idJournal);

                MessageBox.Show("Администратор успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении администратора: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            DialogResult = true;
        }
    }
}
