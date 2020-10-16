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

namespace SoftEnCW
{
    /// <summary>
    /// Interaction logic for MessageCheck.xaml
    /// </summary>
    public partial class MessageCheck : Window
    {
        
        MessageID messageid; //Creating a connection between the MessageID class file.
        public MessageCheck()
        {
            
            messageid = new MessageID();
            InitializeComponent();
            
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string MessageIDConfirm = MessageIDText.Text;
            if (MessageIDConfirm.StartsWith("S") || MessageIDConfirm.StartsWith("s")) //If the messageID starts with an upper or lower case "S"
            {
                
                messageid.messageidstring = MessageIDText.Text; //Store the messageID inside the case file
                string MessageIDString = messageid.messageidstring;
                SMSWindow win3 = new SMSWindow(MessageIDString); //Open the SMS window
                
                win3.Show();

            }
            else if (MessageIDConfirm.StartsWith("E") || MessageIDConfirm.StartsWith("e")) //If the messageID starts with an upper or lower case "E"
            {
                messageid.messageidstring = MessageIDText.Text; //Store the messageID inside the case file
                string MessageIDString = messageid.messageidstring;
                EmailWindow win4 = new EmailWindow(MessageIDString);//Open the SMS window

                win4.Show();
            }
            else if (MessageIDConfirm.StartsWith("T") || MessageIDConfirm.StartsWith("t")) //If the messageID starts with an upper or lower case "T"
            {
                messageid.messageidstring = MessageIDText.Text;//Store the messageID inside the case file
                string MessageIDString = messageid.messageidstring;
                TwitterWindow win5 = new TwitterWindow(MessageIDString);//Open the SMS window

                win5.Show();
            }
            else
            {
                MessageBox.Show("Invalid ID");
            }
           

        }
    }
}
