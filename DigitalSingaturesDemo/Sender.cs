using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DigitalSingaturesDemo
{
	public class Sender
	{
		private string _myRsaKeys = "<RSAKeyValue><Modulus>rW0Prd+S+Z6Wv0gEakgSp/v8Pu4xJ6OjaVCHKTIcf/C5nZvE77454lii3Ne6odV+76oaM2Pn3I9kKehK7CtqklI7rc1+05WRE3u8O5tC5v2ECjEDPMULAcZVTjXSyZtSAOiqk+6nEcJGRED65aGXwFgZuxEY8y4FbUma3I311aM=</Modulus><Exponent>AQAB</Exponent><P>5TYzDyoQBT4C8eqyuWlfNbg0XfnJAUHzonOiz/5az86E9y8V3oxDH3B3GMECDzvcLRJnp5x/G1Lectu1p3ckDw==</P><Q>wbHOTIh7l/p9FszFj/uMdvLlITyABeOZVJEPJhw6fkMSqiRqnx4F2dtqRcGUDBhpWbG6kbTXi9ijMVL8u+iRLQ==</Q><DP>h0KOqvo1bgKEFmJbiZKm/rpvHK3UcguLTGhUwczlpg/G419D1oqK6biib1cmcfrvGSHtTTnKwEMMxlblQafK/Q==</DP><DQ>u80hQFVouF+Xn16mA0eb1s0FWmdlndAin7sSHBpsoHV6CFvMwUCD3cp/TOk3GU8l/mBzi8jy4NYIzM8w2yTQdQ==</DQ><InverseQ>1rYDocFlo3EEs28Miieqa/fE8uzESz6YWONuZPoKHWO/1m9Tf0K01+TtPqDBFRhFBaTNKBJ2lyCGGRIEA41CYg==</InverseQ><D>dZvsciGYbqfZ20ZfmCPgYwNEAPlPZG5Yt2bhAlL1eN4rQnMMjvkWECXD7Lhv3KgIOUfGFOu/pZeoebMKfDbFQe6uA9f4jSYiC3yI0lyGiZQ+SpyJPRKetSSSqiOcK/vnnn2+03RgOVnyU3T52hRXVsb3oXtT5xacWm4IeGABB2E=</D></RSAKeyValue>";
		private string _receiversPublicKey = "<RSAKeyValue><Modulus>vU3Yfu1Z4nFknj9daoDmh+I0CzR+aLnTjUSejQyNJ0IgMb59x4mVe17C6U+bl4Cry7gXAk3LEmmE/BRxjlF8HKlXixoBWak1dpmr89Ye7iaD2UWwl5Dmn07Q9s27NGdywy0BsD1vDcFSgno3LUbVznkw/0hypbnOPxWKlBCao2c=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

		private RSACryptoServiceProvider GetSenderCipher()
		{
			RSACryptoServiceProvider sender = new RSACryptoServiceProvider();
			sender.FromXmlString(_myRsaKeys);			
			return sender;
		}

		private RSACryptoServiceProvider GetReceiverCipher()
		{
			RSACryptoServiceProvider sender = new RSACryptoServiceProvider();
			sender.FromXmlString(_receiversPublicKey);
			return sender;
		}

		public DigitalSignatureResult BuildSignedMessage(string message)
		{
			byte[] messageBytes = Encoding.UTF8.GetBytes(message);
			byte[] cipherBytes = GetReceiverCipher().Encrypt(messageBytes, false);
			byte[] cipherHash = ComputeHashForMessage(cipherBytes);
			byte[] signatureHash = CalculateSignatureBytes(cipherHash);

			string cipher = Convert.ToBase64String(cipherBytes);
			string signature = Convert.ToBase64String(signatureHash);
			return new DigitalSignatureResult() { CipherText = cipher, SignatureText = signature };
		}

		private byte[] ComputeHashForMessage(byte[] cipherBytes)
		{
			SHA1Managed alg = new SHA1Managed();
			byte[] hash = alg.ComputeHash(cipherBytes);
			return hash;
		}

		private byte[] CalculateSignatureBytes(byte[] hashToSign)
		{
			RSAPKCS1SignatureFormatter signatureFormatter = new RSAPKCS1SignatureFormatter(GetSenderCipher());
			signatureFormatter.SetHashAlgorithm("SHA1");
			byte[] signature = signatureFormatter.CreateSignature(hashToSign);
			return signature;
		}
	}
}
