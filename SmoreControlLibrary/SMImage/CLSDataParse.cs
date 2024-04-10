using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SmoreControlLibrary.SMImage
{
    [XmlType(TypeName = "Root")]
    public class CLSDataParse
    {
        [XmlElement("CLSData")]
        public CLSDataElement CLSData { get; set; }

        public class CLSDataElement
        {
            [XmlElement("Total")]
            public UInt64 Total { get; set; }
            [XmlElement("OK")]
            public UInt64 OK { get; set; }
            [XmlElement("NG")]
            public UInt64 NG { get; set; }
        }
    }
}
