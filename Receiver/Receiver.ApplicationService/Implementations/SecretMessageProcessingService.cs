using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Receiver.ApplicationService.Interfaces;
using Receiver.ApplicationService.Requests;
using Receiver.ApplicationService.Responses;
using Receiver.Infrastructure.Cryptography;
using Receiver.Repository.Cryptography;

namespace Receiver.ApplicationService.Implementations
{
	public class SecretMessageProcessingService : ISecretMessageProcessingService
	{
		private readonly IAsymmetricKeyRepositoryFactory _asymmetricKeyRepositoryFactory;
		private readonly IAsymmetricCryptographyService _asymmetricCryptoService;
		private readonly ISymmetricEncryptionService _symmetricCryptoService;

		public SecretMessageProcessingService(IAsymmetricKeyRepositoryFactory asymmetricKeyRepositoryFactory
			, IAsymmetricCryptographyService asymmetricCryptoService, ISymmetricEncryptionService symmetricCryptoService)
		{
			if (asymmetricKeyRepositoryFactory == null) throw new ArgumentNullException("Asymmetric key repository factory.");
			if (asymmetricCryptoService == null) throw new ArgumentNullException("Asymmetric crypto service.");
			if (symmetricCryptoService == null) throw new ArgumentException("Symmetric crypto service.");
			_asymmetricKeyRepositoryFactory = asymmetricKeyRepositoryFactory;
			_asymmetricCryptoService = asymmetricCryptoService;
			_symmetricCryptoService = symmetricCryptoService;
		}

		public ProcessSecretMessageResponse ProcessSecretMessage(ProcessSecretMessageRequest processSecretMessageRequest)
		{
			ProcessSecretMessageResponse response = new ProcessSecretMessageResponse();
			try
			{
				AsymmetricKeyPairGenerationResult asymmetricKeyInStore = _asymmetricKeyRepositoryFactory.Create().FindBy(processSecretMessageRequest.MessageId);
				_asymmetricKeyRepositoryFactory.Create().Remove(processSecretMessageRequest.MessageId);
				String decryptedPublicKey = _asymmetricCryptoService.DecryptCipherText(processSecretMessageRequest.EncryptedPublicKey
					, asymmetricKeyInStore);
				String decryptedMessage = _symmetricCryptoService.Decrypt(processSecretMessageRequest.SymmetricEncryptionArgs.InitialisationVector
					, decryptedPublicKey, processSecretMessageRequest.SymmetricEncryptionArgs.CipherText);
				response.SecretMessageProcessResult = string.Concat("Message received and deciphered: ", decryptedMessage);
			}
			catch (Exception ex)
			{
				response.SecretMessageProcessResult = string.Concat("Exception during the message decryption process: ", ex.Message);
				response.Exception = ex;
			}
			return response;
		}
	}
}
