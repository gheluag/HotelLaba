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
    /// Логика взаимодействия для Madness.xaml
    /// </summary>
    public partial class Madness : Window
    {
        public Madness()
        {
            InitializeComponent();

            this.Closed += CustomMessageBox_Closed;
        }

        private void CustomMessageBox_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
