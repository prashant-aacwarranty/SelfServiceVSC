using System.Xml.Serialization;

namespace AAC.SelfServiceVSC.Models.SCSAPI
{
	[XmlType(Namespace = SoapEnvelope.ta, IncludeInSchema = false)]
	public class SoapEnvelope
	{
		private const string i = "http://www.w3.org/2001/XMLSchema-instance";
		private const string d = "http://www.w3.org/2001/XMLSchema";
		private const string v = "http://schemas.xmlsoap.org/soap/envelope/";
		private const string ta = "http://www.natinc.com/SCSAutoService/";
		private const string tc = "http://tempuri.org/ContractService/";

		[XmlRoot(Namespace = v)]
		public class Envelope
		{
			public Body Body { get; set; }

			static Envelope()
			{
				staticxmlns = new XmlSerializerNamespaces();
				staticxmlns.Add("xsi", i);
				staticxmlns.Add("xsd", d);
				staticxmlns.Add("soap", v);
			}

			private static XmlSerializerNamespaces staticxmlns;

			[XmlNamespaceDeclarations]
			public XmlSerializerNamespaces xmlns { get { return staticxmlns; } set { } }

			[XmlIgnore]
			internal const String URLAuto = "ScsAutoService.asmx";

			[XmlIgnore]
			internal const String URLContract = "ContractService.asmx";

			[XmlIgnore]
			public String SOAPAction
			{
				get
				{
					return Body.GetRates?.SOAPAction ??
						Body.GetTrimsByVIN?.SOAPAction ??
						Body.GetLienholders?.SOAPAction ??
						Body.GenerateContract?.SOAPAction;
				}
			}

			[XmlIgnore]
			public String URL
			{
				get
				{
					return Body.GetRates?.URL ??
						Body.GetTrimsByVIN?.URL ??
						Body.GetLienholders?.URL ??
						Body.GenerateContract?.URL;
				}
			}
		}

		[XmlType(Namespace = v)]
		public class Body
		{
			[XmlElement(ElementName = "GetRates", Namespace = ta)]
			public GetRatesWrapper GetRates { get; set; } = null;

			[XmlElement(ElementName = "GetTrimsByVIN", Namespace = ta)]
			public GetTrimsByVINWrapper GetTrimsByVIN { get; set; } = null;

			[XmlElement(ElementName = "GetLienholders", Namespace = ta)]
			public GetLienholdersWrapper GetLienholders { get; set; } = null;

			[XmlElement(ElementName = "GenerateContract", Namespace = tc)]
			public GenerateContractWrapper GenerateContract { get; set; } = null;

			[XmlElement(ElementName = "VoidContract", Namespace = tc)]
			public VoidContractWrapper VoidContract { get; set; } = null;

			[XmlElement(ElementName = "GetRatesResponse", Namespace = ta)]
			public GetRatesResponseWrapper GetRatesResponse { get; set; } = null;

			[XmlElement(ElementName = "GenerateContractResponse", Namespace = tc)]
			public GenerateContractResponseWrapper GenerateContractResponse { get; set; } = null;

			[XmlElement(ElementName = "VoidContractResponse", Namespace = tc)]
			public VoidContractResponseWrapper VoidContractResponse { get; set; } = null;
		}

		[XmlType(Namespace = ta)]
		public class GetRatesWrapper
		{
			[XmlElement(ElementName = "objGetRatesRequest", Namespace = ta)]
			public GetRates GetRatesRequest { get; set; } = null;

			[XmlIgnore]
			public String SOAPAction
			{
				get
				{
					return "http://www.natinc.com/SCSAutoService/GetRates";
				}
			}

			[XmlIgnore]
			public String URL
			{
				get
				{
					return Envelope.URLAuto;
				}
			}
		}

		[XmlType(Namespace = ta)]
		public class GetTrimsByVINWrapper
		{
			[XmlElement(ElementName = "request", Namespace = ta)]
			public GetTrimsByVIN GetTrimsByVINRequest { get; set; } = null;

			[XmlIgnore]
			public String SOAPAction
			{
				get
				{
					return "http://www.natinc.com/SCSAutoService/GetTrimsByVIN";
				}
			}

			[XmlIgnore]
			public String URL
			{
				get
				{
					return Envelope.URLAuto;
				}
			}
		}

		[XmlType(Namespace = ta)]
		public class GetLienholdersWrapper
		{
			[XmlElement(ElementName = "request", Namespace = ta)]
			public GetLienholders GetLienholders { get; set; } = null;

			[XmlIgnore]
			public String SOAPAction
			{
				get
				{
					return "http://www.natinc.com/SCSAutoService/GetLienholders";
				}
			}

			[XmlIgnore]
			public String URL
			{
				get
				{
					return Envelope.URLAuto;
				}
			}
		}

		[XmlType(Namespace = tc)]
		public class GenerateContractWrapper
		{
			[XmlElement(ElementName = "contract", Namespace = tc)]
			public GenerateContract GenerateContract { get; set; } = null;

			[XmlIgnore]
			public String SOAPAction
			{
				get
				{
					return "http://tempuri.org/ContractService/GenerateContract";
				}
			}

			[XmlIgnore]
			public String URL
			{
				get
				{
					return Envelope.URLContract;
				}
			}
		}

		[XmlType(Namespace = tc)]
		public class VoidContractWrapper
		{
			[XmlElement(ElementName = "VoidContract", Namespace = tc)]
			public VoidContract VoidContract { get; set; } = null;

			[XmlIgnore]
			public String SOAPAction
			{
				get
				{
					return "http://tempuri.org/ContractService/VoidContract";
				}
			}

			[XmlIgnore]
			public String URL
			{
				get
				{
					return Envelope.URLContract;
				}
			}
		}

		[XmlType(Namespace = ta)]
		public class GetRatesResponseWrapper
		{
			[XmlElement(ElementName = "GetRatesResult")]
			public GetRatesResponse GetRatesResponse { get; set; } = null;
		}

		[XmlType(Namespace = tc)]
		public class GenerateContractResponseWrapper
		{
			[XmlElement(ElementName = "GenerateContractResult")]
			public GenerateContractResponse GenerateContractResponse { get; set; } = null;
		}

		[XmlType(Namespace = tc)]
		public class VoidContractResponseWrapper
		{
			[XmlElement(ElementName = "ContractVoid")]
			public VoidContractResponse VoidContractResponse { get; set; } = null;
		}
	}
}
