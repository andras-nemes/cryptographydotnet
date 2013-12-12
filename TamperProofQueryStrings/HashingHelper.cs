using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace TamperProofQueryStrings
{
	public class HashingHelper
	{
		public static readonly string _hashQuerySeparator = "&h=";
		public static readonly string _hashKey = "C2CE6ACD";

		public static string CreateTamperProofQueryString(string basicQueryString)
		{
			return string.Concat(basicQueryString, _hashQuerySeparator, ComputeHash(basicQueryString));
		}

		private static string ComputeHash(string basicQueryString)
		{
			HttpSessionState httpSession = HttpContext.Current.Session;
			basicQueryString += httpSession.SessionID;
			httpSession["HashIndex"] = 10;

			byte[] textBytes = Encoding.UTF8.GetBytes(basicQueryString);
			//HMACSHA1 hashAlgorithm = new HMACSHA1(Conversions.HexToByteArray(_hashKey));
			SHA1 hashAlgorithm = new SHA1Managed();
			byte[] hash = hashAlgorithm.ComputeHash(textBytes);
			return Conversions.ByteArrayToHex(hash);
		}

		public static void ValidateQueryString()
		{
			HttpRequest request = HttpContext.Current.Request;

			if (request.QueryString.Count == 0)
			{
				return;
			}

			string queryString = request.Url.Query.TrimStart(new char[] { '?' });

			string submittedHash = request.QueryString["h"];
			if (submittedHash == null)
			{
				throw new ApplicationException("Querystring validation hash missing!");
			}

			int hashPos = queryString.IndexOf(_hashQuerySeparator);
			queryString = queryString.Substring(0, hashPos);

			//If the hash that was sent on the querystring does not match our compute of that hash given the 
			// current data in the querystring, then throw an exception
			if (submittedHash != ComputeHash(queryString))
			{
				throw new ApplicationException("Querystring hash value mismatch");
			}
		}
	}
}