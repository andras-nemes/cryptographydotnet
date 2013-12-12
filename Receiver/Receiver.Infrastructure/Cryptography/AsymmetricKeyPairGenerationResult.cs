using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Receiver.Infrastructure.Cryptography
{
	public class AsymmetricKeyPairGenerationResult
	{
		private XDocument _fullKeyPairXml;
		private XDocument _publicKeyOnlyXml;

		public AsymmetricKeyPairGenerationResult(XDocument fullKeyPairXml, XDocument publicKeyOnlyXml)
		{
			if (fullKeyPairXml == null) throw new ArgumentNullException("Full key pair XML");
			if (publicKeyOnlyXml == null) throw new ArgumentNullException("Public key only XML");
			_fullKeyPairXml = fullKeyPairXml;
			_publicKeyOnlyXml = publicKeyOnlyXml;
		}

		public XDocument FullKeyPairXml
		{
			get
			{
				return _fullKeyPairXml;
			}
		}

		public XDocument PublicKeyOnlyXml
		{
			get
			{
				return _publicKeyOnlyXml;
			}
		}
	}
}
