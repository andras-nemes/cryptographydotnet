using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace Receiver.Infrastructure.Cryptography
{
	public class RsaCryptographyService : IAsymmetricCryptographyService
	{
		public AsymmetricKeyPairGenerationResult GenerateAsymmetricKeys()
		{
			RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider();
			XDocument publicKeyXml = XDocument.Parse(myRSA.ToXmlString(false));
			XDocument fullKeyXml = XDocument.Parse(myRSA.ToXmlString(true));
			return new AsymmetricKeyPairGenerationResult(fullKeyXml, publicKeyXml);
		}

		public string DecryptCipherText(string cipherText, AsymmetricKeyPairGenerationResult keyPair)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(keyPair.FullKeyPairXml.ToString());
			byte[] original = rsa.Decrypt(Convert.FromBase64String(cipherText), false);
			return Encoding.UTF8.GetString(original);
		}
	}
}
