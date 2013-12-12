using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Receiver.Infrastructure.Cryptography;

namespace Tester
{
	class Program
	{
		static void Main(string[] args)
		{
			IAsymmetricCryptographyService ser = new RsaCryptographyService();
			AsymmetricKeyPairGenerationResult keyPair = ser.GenerateAsymmetricKeys();
		}
	}
}
