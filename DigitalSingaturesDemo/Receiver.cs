using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DigitalSingaturesDemo
{
	public class Receiver
	{
		private string _myRsaKeys = "<RSAKeyValue><Modulus>vU3Yfu1Z4nFknj9daoDmh+I0CzR+aLnTjUSejQyNJ0IgMb59x4mVe17C6U+bl4Cry7gXAk3LEmmE/BRxjlF8HKlXixoBWak1dpmr89Ye7iaD2UWwl5Dmn07Q9s27NGdywy0BsD1vDcFSgno3LUbVznkw/0hypbnOPxWKlBCao2c=</Modulus><Exponent>AQAB</Exponent><P>6veL+pbUjOr0PAiFcvBRwNlTz/+8T1iLHqkCggRPDSsTg25ybSqDa98mP5NQj9LHSYCECjOGZkiN4NoxgPPDxw==</P><Q>zj/l0Z36A/iD2IrVQzrEsvp31cmU6f9VCyPIGiM0FSEXbj23JuPNUPCzSo5oAAiSZfs/hR9uuAx1xQFAfTzjYQ==</Q><DP>dsW7VGh5+OGro80K6BbivIEfBL1ZCyLO8Ciuw9o5u4ZSztU9skETPawHQYvN5WW+p0D3fdCd14ZFcavZ6j1OcQ==</DP><DQ>YSQBRzgjsEkVOCEzjsWYLUAAvwWBiLCEyolgzsaz2hvK4FZa9AspAa1MlJn768Ady8CJS1bhm/fqZA5R5GqQIQ==</DQ><InverseQ>zEGFnyMtfxSYHwRv8nZ4xVcFctnU2pYmmXXYv8NV5FvhZi8Z1f1GE3tmS8qDyIuDTrXjmII2cffLMjPOVmLKoQ==</InverseQ><D>Ii97qDg+oijuDbHNsd0DRIix81AQf+MG9BzvMPOSTgOgAruuxSjwaK4NLsrkgzCGVayx4wWfZXzOuiMK+rN2YPr6IPeut3O14uuwLH7brxkit+MnhclsCtKpdT2iuUGOnbEhWccepCO7YLyyczhT9GE0rEtbEK6S7wvVKab/osE=</D></RSAKeyValue>";
		private string _senderPublicKey = "<RSAKeyValue><Modulus>rW0Prd+S+Z6Wv0gEakgSp/v8Pu4xJ6OjaVCHKTIcf/C5nZvE77454lii3Ne6odV+76oaM2Pn3I9kKehK7CtqklI7rc1+05WRE3u8O5tC5v2ECjEDPMULAcZVTjXSyZtSAOiqk+6nEcJGRED65aGXwFgZuxEY8y4FbUma3I311aM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

		public string ExtractMessage(DigitalSignatureResult signatureResult)
		{
			byte[] cipherTextBytes = Convert.FromBase64String(signatureResult.CipherText);
			byte[] signatureBytes = Convert.FromBase64String(signatureResult.SignatureText);
			byte[] recomputedHash = ComputeHashForMessage(cipherTextBytes);
			VerifySignature(recomputedHash, signatureBytes);
			byte[] plainTextBytes = GetReceiverCipher().Decrypt(cipherTextBytes, false);
			return Encoding.UTF8.GetString(plainTextBytes);
		}

		private RSACryptoServiceProvider GetSenderCipher()
		{
			RSACryptoServiceProvider sender = new RSACryptoServiceProvider();
			sender.FromXmlString(_senderPublicKey);
			return sender;
		}

		private RSACryptoServiceProvider GetReceiverCipher()
		{
			RSACryptoServiceProvider sender = new RSACryptoServiceProvider();
			sender.FromXmlString(_myRsaKeys);
			return sender;
		}

		private byte[] ComputeHashForMessage(byte[] cipherBytes)
		{
			SHA1Managed alg = new SHA1Managed();
			byte[] hash = alg.ComputeHash(cipherBytes);
			return hash;
		}

		private void VerifySignature(byte[] computedHash, byte[] signatureBytes)
		{
			RSACryptoServiceProvider senderCipher = GetSenderCipher();
			RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(senderCipher);
			deformatter.SetHashAlgorithm("SHA1");
			if (!deformatter.VerifySignature(computedHash, signatureBytes))
			{
				throw new ApplicationException("Signature did not match from sender");
			}
		}
	}
}
