using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Receiver.ApplicationService.Interfaces;
using Receiver.ApplicationService.Responses;

namespace Receiver.Web.Controllers
{
    public class PublicKeyController : ApiController
    {
		private readonly IAsymmetricCryptographyApplicationService _asymmetricCryptoApplicationService;

		public PublicKeyController(IAsymmetricCryptographyApplicationService asymmetricCryptoApplicationService)
		{
			if (asymmetricCryptoApplicationService == null) throw new ArgumentNullException("IAsymmetricCryptographyApplicationService");
			_asymmetricCryptoApplicationService = asymmetricCryptoApplicationService;
		}

		public HttpResponseMessage Get()
		{
			ServiceResponseBase response = _asymmetricCryptoApplicationService.GetAsymmetricPublicKey();
			return Request.BuildResponse(response);
		}
    }
}
