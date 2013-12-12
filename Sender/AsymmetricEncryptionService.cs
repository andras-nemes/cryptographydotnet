using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sender
{
	public class AsymmetricEncryptionService
	{
		private RSACryptoServiceProvider _rsaPublicKeyForEncryption;

		public AsymmetricEncryptionService(RSACryptoServiceProvider rsaPublicKeyForEncryption)
		{
			_rsaPublicKeyForEncryption = rsaPublicKeyForEncryption;
		}

		public string GetCipherText(string plainText)
		{
			byte[] data = Encoding.UTF8.GetBytes(plainText);
			byte[] cipherText = _rsaPublicKeyForEncryption.Encrypt(data, false);
			return Convert.ToBase64String(cipherText);
		}
	}
}
