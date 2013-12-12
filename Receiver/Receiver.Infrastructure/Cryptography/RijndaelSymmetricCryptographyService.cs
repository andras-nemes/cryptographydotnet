using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Receiver.Infrastructure.Cryptography
{
	public class RijndaelSymmetricCryptographyService : ISymmetricEncryptionService
	{
		public string Decrypt(string initialisationVector, string publicKey, string cipherText)
		{
			RijndaelManaged cipherForDecryption = CreateCipherForDecryption(initialisationVector, publicKey);
			ICryptoTransform cryptoTransform = cipherForDecryption.CreateDecryptor();
			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			byte[] plainText = cryptoTransform.TransformFinalBlock(cipherTextBytes, 0, cipherTextBytes.Length);
			return Encoding.UTF8.GetString(plainText);
		}

		private RijndaelManaged CreateCipherForDecryption(string initialisationVector, string publicKey)
		{
			RijndaelManaged cipher = new RijndaelManaged();
			cipher.KeySize = CalculateKeySize(publicKey);
			cipher.IV = RecreateInitialisationVector(initialisationVector);
			cipher.BlockSize = 128;
			cipher.Padding = PaddingMode.ISO10126;
			cipher.Mode = CipherMode.CBC;
			byte[] key = HexToByteArray(publicKey);
			cipher.Key = key;
			return cipher;
		}

		private int CalculateKeySize(string publicKey)
		{
			return (publicKey.Length / 2) * 8;
		}

		private byte[] RecreateInitialisationVector(string initialisationVector)
		{
			return Convert.FromBase64String(initialisationVector);
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
