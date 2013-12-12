using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamperProofQueryStrings
{
	public static class Conversions
	{
		public static byte[] HexToByteArray(string hexString)
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

		public static string ByteArrayToHex(byte[] data)
		{
			//Convert bytes to 2 hex characters per byte, remove "-" separators
			return BitConverter.ToString(data);
		}
	}
}