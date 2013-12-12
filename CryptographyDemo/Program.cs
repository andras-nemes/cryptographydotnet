using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			/*
			string plainTextOne = "Hello Crypto";
			string plainTextTwo = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

			SHA512Managed sha512 = new SHA512Managed();
			MACTripleDES des = new MACTripleDES();

			byte[] hashedValueOfTextOne = sha512.ComputeHash(Encoding.UTF8.GetBytes(plainTextOne));
			byte[] hashedValueOfTextTwo = sha512.ComputeHash(Encoding.UTF8.GetBytes(plainTextTwo));

			byte[] test = des.ComputeHash(Encoding.UTF8.GetBytes(plainTextOne));

			string hexOfValueOne = BitConverter.ToString(hashedValueOfTextOne);
			string hexOfValueTwo = BitConverter.ToString(hashedValueOfTextTwo);

			Console.WriteLine(hexOfValueOne);
			Console.WriteLine(hexOfValueTwo);
			 */

			/*
			string salt = GenerateSalt(8);
			Console.WriteLine("Salt: " + salt);
			string password = "secret";
			string constant = "xl1k5ss5NTE=";
			string hashedPassword = ComputeHash(password, salt, constant);

			Console.WriteLine(hashedPassword);
			 */
			
			SymmetricAlgDemo symmDemo = new SymmetricAlgDemo();
			symmDemo.Encrypt("Hello sdfasdfg dfgsdfgdfghfgn bfgbgsdcfsdhmi cmhusimfdcgkcdg");
			symmDemo.Decrypt(symmDemo.IV, symmDemo.CipherText);

			/*
			string validKey = symmDemo.GetValidEncryptionKey(128);
			symmDemo.ChainStreamOperations("test");
			Console.WriteLine(validKey);*/
			
			/*
			AsymmetricAlgoService asymmService = new AsymmetricAlgoService();
			string cipherText = asymmService.GetCipherText("Hello");
			Console.WriteLine(cipherText);
			string original = asymmService.DecryptCipherText(cipherText);
			Console.WriteLine(original);
			 */
			Console.ReadKey();
		}

		public static string ComputeHash(string password, string salt)
		{
			SHA512Managed hashAlg = new SHA512Managed();
			byte[] hash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
			return Convert.ToBase64String(hash);
		}

		public static string ComputeHash(string password, string salt, string entropy)
		{
			SHA512Managed hashAlg = new SHA512Managed();
			byte[] hash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(password + salt + entropy));
			return Convert.ToBase64String(hash);
		}

		private static string GenerateSalt(int byteCount)
		{
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] salt = new byte[byteCount];
			rng.GetBytes(salt);
			return Convert.ToBase64String(salt);
		}
	}
}
