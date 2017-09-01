using Consul;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Nano.SssCore.RegisterConsul
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(HelloConsul().GetAwaiter().GetResult());
			Console.WriteLine(HealthChecks().GetAwaiter());
		}

		public static async Task<string> HelloConsul()
		{
			using (var client = new ConsulClient())
			{
				var putPair = new KVPair("hello")
				{
					Value = Encoding.UTF8.GetBytes("Hello Consul")
				};

				var putAttempt = await client.KV.Put(putPair);

				if (putAttempt.Response)
				{
					var getPair = await client.KV.Get("hello");
					return Encoding.UTF8.GetString(getPair.Response.Value, 0,
						getPair.Response.Value.Length);
				}
				return "";
			}
		}

		public static async Task HealthChecks()
		{
			try
			{
				var client = new ConsulClient(s => new ConsulClientConfiguration
				{
					Address = new Uri("http://localhost:8500")
				});
				var svcID = "daipeng";
				var registration = await client.Agent.ServiceRegister(new AgentServiceRegistration()
				{
					Address = "http://localhost:64245/api/values",
					ID = svcID,
					Name = svcID,
					Tags = new[] { svcID },
					Port = 64245,
					Check = new AgentServiceCheck
					{
						HTTP = "http://localhost:64245/api/values",
						Interval = new TimeSpan(0, 0, 10),
						DeregisterCriticalServiceAfter = new TimeSpan(0, 1, 0),
					}
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
