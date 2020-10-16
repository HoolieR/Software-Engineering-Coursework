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
using System.IO;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace SoftEnCW
{
    /// <summary>
    /// Interaction logic for EmailWindow.xaml
    /// </summary>
    public partial class EmailWindow : Window
    {
        //Setting up the class files to be used as storage.
        EmailDataJSON stringdata;
        MessageID messageidinfo;
        JavaScriptSerializer ser = new JavaScriptSerializer(); //Initializing the JSON serializer.
        public EmailWindow(string messageidstring)
        {
            stringdata = new EmailDataJSON();
            messageidinfo = new MessageID();
            InitializeComponent();
            messageidinfo.messageidstring = messageidstring; //Parsing the MessageID into the Email window.
            this.subjectTextBox.MaxLength = 20; //Setting the max length of the Subject text box.
            this.messageTextBox.MaxLength = 1028; //Setting the max length of the Message text box.

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //Storing the Text box information into the Email class file.
            stringdata.sender = senderTextBox.Text;
            stringdata.subject = subjectTextBox.Text;
            stringdata.bodytext = messageTextBox.Text;
            string input = stringdata.bodytext; //Creates a variable to store the body text information.
            var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase); //Creates a new custom regular expression to catch the URL.


            if (messageTextBox.Text.Contains("www.")) //If the textbox contains a URL - 
            {
                foreach (Match matchurl in linkParser.Matches(input)) //Parse the Input from the text box into the custom Regex variable.
                {
                    Debug.WriteLine(matchurl.Value); //Debug the URL first.
                    string url = matchurl.Value; //store the parsed URL into a variable
                    quarURLbox.Text = url; //Display the removed URL into the text box.
                    string RemovedURL = stringdata.bodytext.Replace(url, string.Empty); //Remove the parsed URL from the text box.
                    messageTextBox.Text = RemovedURL; //re-insert the adjusted message back into the text box.
                }
                string quar = "URL Quarantined";
                stringdata.bodytext = Regex.Replace(messageTextBox.Text, "www.", string.Empty);
                stringdata.url = quar;
                EmailDataJSON stringPass = new EmailDataJSON() { sender = senderTextBox.Text, subject = subjectTextBox.Text, bodytext = messageTextBox.Text, url = quar }; //Store the data into the JSON.
                string outputJSON = ser.Serialize(stringPass); //Serialize the JSON
                File.WriteAllText(messageidinfo.messageidstring + ".json", outputJSON); //Output the JSON
            }
            else //Otherwise output the JSON with no quarantined URL.
            {
                string quar2 = "No URL Quarantined";
                stringdata.url = quar2;
                EmailDataJSON stringPass = new EmailDataJSON() { sender = senderTextBox.Text, subject = subjectTextBox.Text, bodytext = messageTextBox.Text, url = quar2 };
                string outputJSON = ser.Serialize(stringPass);
                File.WriteAllText(messageidinfo.messageidstring + ".json", outputJSON);
 
            }

        }

        private void loadButton_Click(object sender, RoutedEventArgs e) //When the load button is clicked.
        {
            String EmailJSONString = File.ReadAllText(messageidinfo.messageidstring + ".json"); //Read from the JSON file
            EmailDataJSON stringLoad = ser.Deserialize<EmailDataJSON>(EmailJSONString); //Deserialize the URL
            //Display the de-serialized info into the text boxes.
            senderTextBox.Text = stringLoad.sender;
            subjectTextBox.Text = stringLoad.subject;
            messageTextBox.Text = stringLoad.bodytext;
            var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase); //Parse the Removed link
            string input = stringLoad.bodytext;
                foreach (Match matchurl in linkParser.Matches(input))
                {
                    Debug.WriteLine(matchurl.Value);
                    string url = matchurl.Value;
                    quarURLbox.Text = url;
                string RemovedURL = stringdata.bodytext.Replace(url, string.Empty);
                    messageTextBox.Text = RemovedURL;
                }
            Debug.WriteLine(stringLoad);  

            
        }

        
    }
}
