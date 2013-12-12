using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;


namespace Sender
{
	public class PublicKeyResponse
	{
		public Guid MessageId { get; set; }
		public RSACryptoServiceProvider RsaPublicKey { get; set; }
	}
}
