using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receiver.ApplicationService.Requests
{
	public class ProcessSecretMessageRequest
	{
		public SymmetricEncryptionArguments SymmetricEncryptionArgs { get; set; }
		public string EncryptedPublicKey { get; set; }
		public Guid MessageId { get; set; }
	}
}
