using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Receiver.ApplicationService.Interfaces;
using Receiver.ApplicationService.Responses;
using Receiver.Infrastructure.Cryptography;
using Receiver.Repository.Cryptography;

namespace Receiver.ApplicationService.Implementations
{
	public class AsymmetricCryptographyApplicationService : IAsymmetricCryptographyApplicationService
	{
		private readonly IAsymmetricCryptographyService _cryptographyInfrastructureService;
		private readonly IAsymmetricKeyRepositoryFactory _asymmetricKeyRepositoryFactory;

		public AsymmetricCryptographyApplicationService(IAsymmetricCryptographyService cryptographyInfrastructureService
			, IAsymmetricKeyRepositoryFactory asymmetricKeyRepositoryFactory)
		{
			if (cryptographyInfrastructureService == null) throw new ArgumentNullException("IAsymmetricCryptographyService");
			if (asymmetricKeyRepositoryFactory == null) throw new ArgumentNullException("IAsymmetricKeyRepositoryFactory");
			_cryptographyInfrastructureService = cryptographyInfrastructureService;
			_asymmetricKeyRepositoryFactory = asymmetricKeyRepositoryFactory;
		}

		public GetAsymmetricPublicKeyResponse GetAsymmetricPublicKey()
		{
			GetAsymmetricPublicKeyResponse publicKeyResponse = new GetAsymmetricPublicKeyResponse();
			try
			{
				AsymmetricKeyPairGenerationResult asymmetricKeyPair = _cryptographyInfrastructureService.GenerateAsymmetricKeys();
				Guid messageId = Guid.NewGuid();
				publicKeyResponse.MessageId = messageId;
				publicKeyResponse.PublicKeyXml = asymmetricKeyPair.PublicKeyOnlyXml;
				_asymmetricKeyRepositoryFactory.Create().Add(messageId, asymmetricKeyPair);
			}
			catch (Exception ex)
			{
				publicKeyResponse.Exception = ex;
			}
			return publicKeyResponse;
		}
	}
}
