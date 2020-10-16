using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftEnCW
{
    class StringDataJSON
    {
        public string sender //Stores the sender name
        {
            get;
            set;

        }
        public string bodytext //stores the body text information.
        {
            get;
            set;
        }
        public override string ToString() //stores the data into the JSON format.
        {
            return string.Format("Sender: {0} \nBody: {1}", sender, bodytext);
        }
    }
}
