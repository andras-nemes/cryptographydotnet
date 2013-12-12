using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Receiver.ApplicationService.Requests;
using Receiver.ApplicationService.Responses;

namespace Receiver.ApplicationService.Interfaces
{
	public interface ISecretMessageProcessingService
	{
		ProcessSecretMessageResponse ProcessSecretMessage(ProcessSecretMessageRequest processSecretMessageRequest);
	}
}
