using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Receiver.ApplicationService.Responses;

namespace Receiver.ApplicationService.Interfaces
{
	public interface IAsymmetricCryptographyApplicationService
	{
		GetAsymmetricPublicKeyResponse GetAsymmetricPublicKey();
	}
}
