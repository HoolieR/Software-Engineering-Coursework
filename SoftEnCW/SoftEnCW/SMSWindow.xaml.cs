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
using System.Web.Script.Serialization;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace SoftEnCW
{
    /// <summary>
    /// Interaction logic for SMSWindow.xaml
    /// </summary>
    /// 


   
    public partial class SMSWindow : Window
    {
        //Creates a connection between the sms window and the case files.
        StringDataJSON stringdata; 
        MessageID messageidinfo;
        JavaScriptSerializer ser = new JavaScriptSerializer(); //Begins the JSON Serializer. 
        public SMSWindow(string messageidstring)
        {
            stringdata = new StringDataJSON();
            messageidinfo = new MessageID();         
            InitializeComponent();
            messageidinfo.messageidstring = messageidstring;
            this.SenderText.MaxLength = 11; //Setting the limit of the sender info box.
            this.BodyText.MaxLength = 140; //Setting the limit of the body info box.
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {

            var dict = File.ReadLines("textwords.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]); //Stores the Text Speak CSV file into a dictionary
            //Stores the information from the text boxes into the case files.
            stringdata.sender = SenderText.Text;
            stringdata.bodytext = BodyText.Text;
            

            

            var texts = new List<String> //Creates a new list for the text messages.
            {
                stringdata.bodytext
            };

            foreach (var text in texts) //For loop to find the text speak info and swap it.
            {
                
                var result = string.Join(" ", text.Split(' ').Select(i => dict.ContainsKey(i.ToComparable()) ? dict[i.ToComparable()] : i)); //The result is searching the dictionary for the Key and Field - the Key being the shortened Text speech, while the field being the full text.
                Debug.WriteLine(result); //Debugs the result.
                StringDataJSON stringPass = new StringDataJSON() { sender = SenderText.Text, bodytext = result }; //Stores the data into the JSON file
                string outputJSON = ser.Serialize(stringPass); //Serializes the JSON file.
                File.WriteAllText(messageidinfo.messageidstring + ".json", outputJSON); //Saves the JSON file.
            }
            
        }

        public void button_Click_1(object sender, RoutedEventArgs e)
        {
            
            String SMSJSONString = File.ReadAllText(messageidinfo.messageidstring + ".json"); //Loads the JSON file based on the name of the Message ID.
            StringDataJSON stringLoad = ser.Deserialize<StringDataJSON>(SMSJSONString); //Deserializes the JSON file.
            Debug.WriteLine(stringLoad);
            SenderText.Text = stringLoad.sender; //Displays the JSON information into the text boxes.
            BodyText.Text = stringLoad.sender;
            
        }

    }
    public static class StringExtensions
    {
        public static string ToComparable(this string source) => source.Replace(",", string.Empty).ToUpper(); //Removes the comma from the CSV file.
    }
}
