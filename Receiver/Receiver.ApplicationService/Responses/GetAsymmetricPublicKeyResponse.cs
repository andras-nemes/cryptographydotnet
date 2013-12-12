using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Receiver.ApplicationService.Responses
{
	public class GetAsymmetricPublicKeyResponse : ServiceResponseBase
	{
		public Guid MessageId { get; set; }
		public XDocument PublicKeyXml { get; set; }
	}
}
