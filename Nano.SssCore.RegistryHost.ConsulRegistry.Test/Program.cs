using Nano.SssCore.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nano.SssCore.RegistryHost.ConsulRegistry.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			var re = new ConsulRegistry();
			re.RegisterServiceAsync().Wait();
			Console.WriteLine("Hello World!");
		}
	}
	public class ConsulRegistry
	{
		private readonly IRegistryHost _registryHost;
		public ConsulRegistry()
		{
			_registryHost = new ConsulRegistryHost(new ConsulRegistryHostConfiguration());
		}

		public async Task RegisterServiceAsync()
		{
			var serviceName = nameof(ConsulRegistry);
			await _registryHost.RegisterServiceAsync(serviceName, serviceName, new Uri("http://localhost:64245"), new Uri("http://localhost:64245/api/Hello/Index"));

			Func<string, Task<RegistryInformation>> findTenant = async s => (await ((ConsulRegistryHost)_registryHost).FindAllServicesAsync())
				.FirstOrDefault(x => x.Name == s);

			//var tenant = findTenant(serviceName).Result;
			//await _registryHost.DeregisterServiceAsync(tenant.Id);
		}

	}
}
