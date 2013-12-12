using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sender
{
	public class SendMessageArguments
	{
		public SymmetricEncryptionResult SymmetricEncryptionArgs { get; set; }
		public string EncryptedPublicKey { get; set; }
		public Guid MessageId { get; set; }
	}
}
