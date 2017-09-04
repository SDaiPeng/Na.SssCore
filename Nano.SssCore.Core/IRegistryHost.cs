using System;
using System.Collections.Generic;
using System.Text;

namespace Nano.SssCore.Core
{
	public interface IRegistryHost : IManageServiceInstances,
			IManageHealthChecks,
			IResolveServiceInstances,
			IHaveKeyValues
	{
	}
}
