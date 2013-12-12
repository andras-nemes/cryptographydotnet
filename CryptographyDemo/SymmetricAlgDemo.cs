using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyDemo
{
	public class SymmetricAlgDemo
	{
		private string _fileName = @"c:\temp\result.txt";
		public string IV { get; set; }
		public string CipherText { get; set; }


		public void Encrypt(string plainText)
		{
			RijndaelManaged rijndael = CreateCipher();
			string initVector = Convert.ToBase64String(rijndael.IV);
			Console.WriteLine("Rijndael init vector: " + initVector + ", length: " + initVector.Length);
			ICryptoTransform cryptoTransform = rijndael.CreateEncryptor();
			byte[] plain = Encoding.UTF8.GetBytes(plainText);
			byte[] cipherText = cryptoTransform.TransformFinalBlock(plain, 0, plain.Length);
			string cipher = Convert.ToBase64String(cipherText);
			Console.WriteLine("Cipher text: " + cipher + ", length: " + cipher.Length);
			CipherText = Convert.ToBase64String(cipherText);
			IV = Convert.ToBase64String(rijndael.IV);
		}

		private RijndaelManaged CreateCipher()
		{
			RijndaelManaged cipher = new RijndaelManaged();
			cipher.KeySize = 128;
			cipher.BlockSize = 128;
			cipher.Padding = PaddingMode.ISO10126;
			cipher.Mode = CipherMode.CBC;
			byte[] key = HexToByteArray("8B50BB68A8162E3D1E102556D2853291");
			cipher.Key = key;
			return cipher;
		}

		public void GenerateRandomBytes(byte[] buffer)
		{
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			rng.GetBytes(buffer);
		}

		public string GetValidEncryptionKey(int bitSize)
		{
			byte[] key = new byte[bitSize / 8];
			GenerateRandomBytes(key);
			return BitConverter.ToString(key).Replace("-", string.Empty);
		}

		public void ChainStreamOperations(string plainText)
		{
			byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
			RijndaelManaged cipher = CreateCipher();
			using (FileStream cipherFile = new FileStream(_fileName, FileMode.Create, FileAccess.Write))
			{
				ICryptoTransform base64CryptoTransform = new ToBase64Transform();
				ICryptoTransform cipherTransform = cipher.CreateEncryptor();
				using (CryptoStream firstCryptoStream = new CryptoStream(cipherFile, base64CryptoTransform, CryptoStreamMode.Write))
				{
					using (CryptoStream secondCryptoStream = new CryptoStream(firstCryptoStream, cipherTransform, CryptoStreamMode.Write))
					{
						secondCryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
					}
				}
			}
		}

		public void Decrypt(string iv, string cipherText)
		{
			RijndaelManaged cipher = CreateCipher();
			cipher.IV = Convert.FromBase64String(iv);
			ICryptoTransform cryptTransform = cipher.CreateDecryptor();
			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			byte[] plainText = cryptTransform.TransformFinalBlock(cipherTextBytes, 0, cipherTextBytes.Length);

			Console.WriteLine("Decrypted message: " + Encoding.UTF8.GetString(plainText));
		}

		public byte[] HexToByteArray(string hexString)
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
