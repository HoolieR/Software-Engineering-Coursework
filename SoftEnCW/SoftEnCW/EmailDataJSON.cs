using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftEnCW
{
    class EmailDataJSON
    {
        public string sender //stores the "Sender" information
        {
            get;
            set;
        }

        public string subject //Stores the "subject" information
        {
            get;
            set;
        }
        public string bodytext //Stores the body text information
        {
            get;
            set;
        }

        public string url //Stores the URL information
        {
            get;
            set;
        }
        public override string ToString()
        {
            return string.Format("Sender: {0} \nSubject: {1} \nBody: {1}", sender, subject, bodytext); //Sets the format for the JSON file.
        }
    }
}
