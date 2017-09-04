using System;
using System.Collections.Generic;
using System.Text;

namespace Nano.SssCore.Core
{
	/// <summary>
	/// 注册Web API服务实例
	/// </summary>
	public interface IRegistryTenant
	{
		Uri Uri { get; }
	}
}
