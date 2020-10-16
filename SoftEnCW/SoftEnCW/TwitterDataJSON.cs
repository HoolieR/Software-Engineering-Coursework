using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftEnCW
{
    class TwitterDataJSON
    {
        public string sender //stores the sender information.
        {
            get;
            set;
        }
        public string bodytext //Stores the body text information.
        {
            get;
            set;
        }
        public override string ToString() //stores the information in the correct JSON format.
        {
            return string.Format("Sender: {0} \nBody: {1}", sender, bodytext);
        }

    }
}
