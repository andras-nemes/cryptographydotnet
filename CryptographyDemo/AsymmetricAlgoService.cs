using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyDemo
{
	public class AsymmetricAlgoService
	{
		private const string _rsaKeyForDecryption = @"<RSAKeyValue>
	<Modulus>sZxoLV6wFGFbHGAveQq+wnEBZKI5hUzcdl0/rKw+eIRGj7U8h9gsCTM7jly/n/NGTV9vSk4AZKQ0zF+DoH8irPT22kqahKI8tc+dYBUe5xPXWAC6J4qmZj/mWTfLrQ5kwmoxxumtAku4hQLptkiv5b82M4FQKUKswp227FibLQ0=</Modulus>
	<Exponent>AQAB</Exponent>
	<P>7YO7CSQ8KFe1GpoQ7rDPjQSVqcas6+KvDbq7LIQmzKmKYGJv1F5G9wQ6v5vtj9S/WbHs7sCdmPKhmloSxold1Q==</P>
	<Q>v28mRwbjmjpagn3FX0DUqBAth4ppdHrw8t6a7PzLqNo9dXpJIyGpVb+bl4ShgGi2+p8LpPMS+pkA9hnz3gRWWQ==</Q>
	<DP>6punQUVggrz37/nk5dESgonXx6auoiX8ogQj5Ln4lUqWm9RAADbLxC2SVjgQdXVBObkNf1wVj8GCrTNxvlhrrQ==</DP>
	<DQ>h03XPyJ2Yj7WVB7zDtUyuSreE0vYJ1Tx0qdV1yUCCXFfORZadTNIjWvlXB2JTMo2ckNRpp+LjYXxMQC85fIo2Q==</DQ>
	<InverseQ>xBaODyhFRLkkTPh1nonpuuQulE41w60hy1/FtwWW2iVAJKJ2SG64s6gZ5VzqOv9TnuCTxrB81lI3dVZwaAdZLg==</InverseQ>
	<D>lmjqIGHro5a+3czm5w6edXPVxi9LnwN0QBi/767+SHlceB73X+NFh5UHfow1C3OtuaB0UuyzkIcu31ST17tS1NJr6tHKvBZeIdjvFbrDLboP9o2fYJX6g0jgIx/oERWekRZE4UaRvIe5FqML+lbnMkvITJskw18sIkSYvoY4UoE=</D>
</RSAKeyValue>";

		private const string _rsaKeyForEncryption = @"<RSAKeyValue>
	<Modulus>sZxoLV6wFGFbHGAveQq+wnEBZKI5hUzcdl0/rKw+eIRGj7U8h9gsCTM7jly/n/NGTV9vSk4AZKQ0zF+DoH8irPT22kqahKI8tc+dYBUe5xPXWAC6J4qmZj/mWTfLrQ5kwmoxxumtAku4hQLptkiv5b82M4FQKUKswp227FibLQ0=</Modulus>
	<Exponent>AQAB</Exponent>
</RSAKeyValue>";

		private RSACryptoServiceProvider CreateCipherForEncryption()
		{
			RSACryptoServiceProvider cipher = new RSACryptoServiceProvider();			
			cipher.FromXmlString(_rsaKeyForEncryption);
			return cipher;
		}

		private RSACryptoServiceProvider CreateCipherForDecryption()
		{
			RSACryptoServiceProvider cipher = new RSACryptoServiceProvider();
			cipher.FromXmlString(_rsaKeyForDecryption);				
			return cipher;
		}

		public string GetCipherText(string plainText)
		{
			RSACryptoServiceProvider cipher = CreateCipherForEncryption();
			byte[] data = Encoding.UTF8.GetBytes(plainText);
			byte[] cipherText = cipher.Encrypt(data, false);
			return Convert.ToBase64String(cipherText);
		}

		public string DecryptCipherText(string cipherText)
		{
			RSACryptoServiceProvider cipher = CreateCipherForDecryption();
			byte[] original = cipher.Decrypt(Convert.FromBase64String(cipherText), false);
			return Encoding.UTF8.GetString(original);
		}

		public void ProgrammaticRsaKeys()
		{
			RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider();
			RSAParameters publicKey = myRSA.ExportParameters(false);
			string xml = myRSA.ToXmlString(false);
		}
	}
}
