using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receiver.Repository.Cryptography
{
	public interface IAsymmetricKeyRepositoryFactory
	{
		InMemoryAsymmetricKeyRepository Create();
	}
}
