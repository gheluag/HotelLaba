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
    /// Логика взаимодействия для RegJournalPage.xaml
    /// </summary>
    public partial class RegJournalPage : Page
    {
        DataBase db = new DataBase();
        public RegJournalPage()
        {
            InitializeComponent();
            LoadJournal();
        }

        private void LoadJournal()
        {
            var journallist = db.GetRegJournal();
            journalLB.ItemsSource = journallist;
        }

        private void addRoomer_Click(object sender, RoutedEventArgs e)
        {
            AddJournal addJournalEntryWindow = new AddJournal();

            if (addJournalEntryWindow.ShowDialog() == true)
            {
                LoadJournal();
            }
        }
    }
}
