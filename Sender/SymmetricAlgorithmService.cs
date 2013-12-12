using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sender
{
	public class SymmetricAlgorithmService
	{
		private readonly string _mySymmetricPublicKey = ConfigurationManager.AppSettings["RijndaelManagedKey"];

		public SymmetricEncryptionResult Encrypt(string plainText)
		{
			SymmetricEncryptionResult res = new SymmetricEncryptionResult();
			RijndaelManaged rijndael = CreateCipher();
			res.InitialisationVector = Convert.ToBase64String(rijndael.IV);
			ICryptoTransform cryptoTransform = rijndael.CreateEncryptor();
			byte[] plain = Encoding.UTF8.GetBytes(plainText);
			byte[] cipherText = cryptoTransform.TransformFinalBlock(plain, 0, plain.Length);
			res.CipherText = Convert.ToBase64String(cipherText);
			return res;
		}

		private RijndaelManaged CreateCipher()
		{
			RijndaelManaged cipher = new RijndaelManaged();
			cipher.KeySize = 128;
			cipher.BlockSize = 128;
			cipher.Padding = PaddingMode.ISO10126;
			cipher.Mode = CipherMode.CBC;
			byte[] key = HexToByteArray(_mySymmetricPublicKey);
			cipher.Key = key;
			return cipher;
		}

		private byte[] HexToByteArray(string hexString)
		{
			if (0 != (hexString.Length % 2))
			{
				throw new ApplicationException("Hex string must be multiple of 2 in length");
			}

			int byteCount = hexString.Length / 2;
			byte[] byteValues = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{				
				byteValues[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
			}

			return byteValues;
		}
	}
}
