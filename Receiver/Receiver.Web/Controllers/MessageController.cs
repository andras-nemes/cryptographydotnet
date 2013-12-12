using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Receiver.ApplicationService.Interfaces;
using Receiver.ApplicationService.Requests;
using Receiver.ApplicationService.Responses;

namespace Receiver.Web.Controllers
{
    public class MessageController : ApiController
    {
		private readonly ISecretMessageProcessingService _secretMessageProcessingService;

		public MessageController(ISecretMessageProcessingService secretMessageProcessingService)
		{
			if (secretMessageProcessingService == null) throw new ArgumentNullException("ISecretMessageProcessingService");
			_secretMessageProcessingService = secretMessageProcessingService;
		}

		public HttpResponseMessage Post(ProcessSecretMessageRequest secretMessageRequest)
		{
			ServiceResponseBase response = _secretMessageProcessingService.ProcessSecretMessage(secretMessageRequest);
			return Request.BuildResponse(response);
		}
    }
}
