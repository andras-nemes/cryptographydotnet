using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receiver.Infrastructure.Cryptography
{
	public interface ISymmetricEncryptionService
	{
		string Decrypt(string initialisationVector, string publicKey, string cipherText);
	}
}
