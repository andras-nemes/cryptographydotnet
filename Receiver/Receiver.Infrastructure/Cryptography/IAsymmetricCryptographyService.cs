using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Receiver.Infrastructure.Cryptography
{
	public interface IAsymmetricCryptographyService
	{
		AsymmetricKeyPairGenerationResult GenerateAsymmetricKeys();
		string DecryptCipherText(string cipherText, AsymmetricKeyPairGenerationResult keyPair);
	}
}
