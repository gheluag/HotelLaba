using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelApp_net8;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void rooms_btn_Click(object sender, RoutedEventArgs e)
    {
        mainframe.Navigate(new Pages.RoomsPage());
    }

    private void roomers_btn_Click(object sender, RoutedEventArgs e)
    {
        mainframe.Navigate(new Pages.RoomersPage());
    }

    private void admins_btn_Click(object sender, RoutedEventArgs e)
    {
        mainframe.Navigate(new Pages.AdminPage());
    }

    private void journal_btn_Click(object sender, RoutedEventArgs e)
    {
        mainframe.Navigate(new Pages.RegJournalPage());
    }
}