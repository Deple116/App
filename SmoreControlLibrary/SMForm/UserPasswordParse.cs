using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SmoreControlLibrary.SMForm
{
    public enum IDName
    {
        Operator=0,
        Engineer=1,
        Admin=2

    }


    [XmlType(TypeName = "Root")]
    public class UserPasswordParse
    {
        public static int ErrorOK = 0;
        public static int ErrorFailed = -1;

        [XmlElement("OperatorPassword")]
        public OperatorPasswordElement OperatorPassword { get; set; }

        public class OperatorPasswordElement
        {
            [XmlElement("Password")]
            public string Password { get; set; }
        }


        [XmlElement("EngineerPassword")]
        public EngineerPasswordElement EngineerPassword { get; set; }

        public class EngineerPasswordElement
        {
            [XmlElement("Password")]
            public string Password { get; set; }
        }


        [XmlElement("AdminPassword")]
        public AdminPasswordElement AdminPassword { get; set; }

        public class AdminPasswordElement
        {
            [XmlElement("Password")]
            public string Password { get; set; }
        }
    }
}
