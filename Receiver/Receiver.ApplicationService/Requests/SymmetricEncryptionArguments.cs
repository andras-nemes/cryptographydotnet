using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receiver.ApplicationService.Requests
{
	public class SymmetricEncryptionArguments
	{
		public string CipherText { get; set; }
		public string InitialisationVector { get; set; }
	}
}
