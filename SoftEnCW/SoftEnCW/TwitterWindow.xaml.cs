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
    /// Interaction logic for TwitterWindow.xaml
    /// </summary>
    public partial class TwitterWindow : Window
    {
        //Creating a connection between the window and class files.
        TwitterDataJSON stringdata;
        MessageID messageidinfo;
        JavaScriptSerializer ser = new JavaScriptSerializer(); //Starts the JSON serializer.
        public TwitterWindow(string messageidstring)
        {
            stringdata = new TwitterDataJSON();
            messageidinfo = new MessageID();
            InitializeComponent();
            this.senderTextBox.MaxLength = 15; //Sets the max length of the sender box.
            this.messageTextBox.MaxLength = 140; //Sets the max length of the message box.
            messageidinfo.messageidstring = messageidstring; //Parses the Message ID into the widnow.
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var dict = File.ReadLines("textwords.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]); //Creates a data dictionary to store the text speech CSV.
            stringdata.sender = senderTextBox.Text; //stores the text box information into the get sets inside the case files.
            stringdata.bodytext = messageTextBox.Text;


            var texts = new List<String> //creates a new item list of the text message inside the body message.
            {
                stringdata.bodytext
            };
            foreach (var text in texts) //Starts a for loop for the data dictionary.
            {

                var result = string.Join(" ", text.Split(' ').Select(i => dict.ContainsKey(i.ToComparable()) ? dict[i.ToComparable()] : i)); //The result is searching the dictionary for the Key and Field - the Key being the shortened Text speech, while the field being the full text.
                Debug.WriteLine(result);
                TwitterDataJSON stringPass = new TwitterDataJSON() { sender = senderTextBox.Text, bodytext = messageTextBox.Text }; //Stores the data into the JSON file
                string outputJSON = ser.Serialize(stringPass); //Serializes the JSON file.
                File.WriteAllText(messageidinfo.messageidstring + ".json", outputJSON);  //Saves the JSON file.
            }

            string input = stringdata.bodytext;
            foreach (Match matchhashtag in Regex.Matches(input, "(\\#\\w+)")) //Performs a REGEX MATCH to discover the use of a hashtag in a tweet based on the character use.
            {
                //MessageBox.Show("Hashtag found!");;
                Debug.WriteLine(matchhashtag.Groups[1].Value);
                string hashtag = matchhashtag.Groups[1].Value; //creates a new string based on the hashtag found in the tweet.
                hashtagBox.Text = hashtag; //Displays the hashtag in the hash tag text box.
            }
            foreach (Match matchmention in Regex.Matches(input, "(\\@\\w+)")) //Performs a REGEX MATCH to discover the use of a mention in a tweet based on the character use.
            {
                //MessageBox.Show("Mention found!");
                Debug.WriteLine(matchmention.Groups[1].Value);
                string mention = matchmention.Groups[1].Value; //creates a new string based on the mention found in the tweet.
                mentionBox.Text = mention; //Displays the mention in the mention text box.
            }



        }

        private void loadButton_Click_1(object sender, RoutedEventArgs e)
        {

            String TwitterJSONString = File.ReadAllText(messageidinfo.messageidstring + ".json"); //Loads the JSON file name based on the Message ID.
            TwitterDataJSON stringLoad = ser.Deserialize<TwitterDataJSON>(TwitterJSONString); //De-serializes the JSON file
            senderTextBox.Text = stringLoad.sender; //Inserts the JSON file data into the text boxes.
            messageTextBox.Text = stringLoad.bodytext;

            Debug.WriteLine(stringLoad);
        }


    }

   
}



