using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sender
{
	public class SymmetricEncryptionResult
	{
		public string CipherText { get; set; }
		public string InitialisationVector { get; set; }
	}
}
