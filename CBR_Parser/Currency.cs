using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CBR_Parser
{
	[XmlRoot(ElementName = "ValuteCursOnDate")]
	public class Currency
    {
		[XmlElement(ElementName = "Vname")]
		public string Name { get; set; }
		[XmlElement(ElementName = "Vnom")]
		public string Nominal { get; set; }
		[XmlElement(ElementName = "Vcurs")]
		public string Curs { get; set; }
		[XmlElement(ElementName = "Vcode")]
		public string Vcode { get; set; }
		[XmlElement(ElementName = "VchCode")]
		public string VchCode { get; set; }
	}
}
