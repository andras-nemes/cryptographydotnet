using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Receiver.Infrastructure.Cryptography;

namespace Receiver.Repository.Cryptography
{
	public class InMemoryAsymmetricKeyRepository : IAsymmetricKeyRepository
	{
		private Dictionary<Guid, AsymmetricKeyPairGenerationResult> _asymmetricKeyPairs;

		public InMemoryAsymmetricKeyRepository()
		{
			_asymmetricKeyPairs = new Dictionary<Guid, AsymmetricKeyPairGenerationResult>();
		}

		public void Add(Guid messageId, AsymmetricKeyPairGenerationResult asymmetricKeyPair)
		{
			_asymmetricKeyPairs[messageId] = asymmetricKeyPair;
		}

		public AsymmetricKeyPairGenerationResult FindBy(Guid messageId)
		{
			if (_asymmetricKeyPairs.ContainsKey(messageId))
			{
				return _asymmetricKeyPairs[messageId];
			}
			throw new KeyNotFoundException("Invalid message ID.");
		}

		public void Remove(Guid messageId)
		{
			if (_asymmetricKeyPairs.ContainsKey(messageId))
			{
				_asymmetricKeyPairs.Remove(messageId);
			}
		}

		public static InMemoryAsymmetricKeyRepository Instance
		{
			get
			{
				return Nested.instance;
			}
		}

		private class Nested
		{
			static Nested()
			{
			}
			internal static readonly InMemoryAsymmetricKeyRepository instance = new InMemoryAsymmetricKeyRepository();
		}
	}
}
