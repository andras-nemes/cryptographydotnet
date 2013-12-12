using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Receiver.Infrastructure.Cryptography;

namespace Receiver.Repository.Cryptography
{
	public interface IAsymmetricKeyRepository
	{
		void Add(Guid messageId, AsymmetricKeyPairGenerationResult asymmetricKeyPair);
		AsymmetricKeyPairGenerationResult FindBy(Guid messageId);
		void Remove(Guid messageId);
	}
}
