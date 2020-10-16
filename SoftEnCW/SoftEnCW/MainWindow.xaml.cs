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

namespace SoftEnCW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StringDataJSON stringdata;
        public MainWindow()
        {
            StringDataJSON stringdata;
            stringdata = new StringDataJSON();
            InitializeComponent();
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageCheck win2 = new MessageCheck();
            win2.Show();
        }
    }
}
