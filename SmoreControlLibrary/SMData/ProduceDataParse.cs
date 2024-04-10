using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SmoreControlLibrary.SMData
{
	[XmlType(TypeName = "Root")]
	public class ProduceDataParse
	{
		public static int ErrorOK = 0;
		public static int ErrorFailed = -1;
		
		[XmlElement("ProduceData")]
		public ProduceDataElement ProduceData { get; set; }

        public class ProduceDataElement
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
