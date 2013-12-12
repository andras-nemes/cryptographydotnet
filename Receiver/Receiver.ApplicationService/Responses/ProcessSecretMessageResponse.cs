using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receiver.ApplicationService.Responses
{
	public class ProcessSecretMessageResponse : ServiceResponseBase
	{
		public string SecretMessageProcessResult { get; set; }
	}
}
