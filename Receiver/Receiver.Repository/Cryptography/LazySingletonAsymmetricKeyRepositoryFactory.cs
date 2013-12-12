using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receiver.Repository.Cryptography
{
	public class LazySingletonAsymmetricKeyRepositoryFactory : IAsymmetricKeyRepositoryFactory
	{
		public InMemoryAsymmetricKeyRepository Create()
		{
			return InMemoryAsymmetricKeyRepository.Instance;
		}
	}
}
