using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalSingaturesDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			Sender sender = new Sender();
			DigitalSignatureResult res = sender.BuildSignedMessage("Hello digital sig!");
			Console.WriteLine(res.CipherText);
			Console.WriteLine(res.SignatureText);

			String decryptedText = new Receiver().ExtractMessage(res);
			Console.WriteLine(decryptedText);

			Console.ReadKey();
		}
	}
}
