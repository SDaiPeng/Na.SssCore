using CacheManager.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using Microsoft.Extensions.Configuration;

namespace NanoSssCore
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddJsonFile("configuration.json")
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			Action<ConfigurationBuilderCachePart> settings = (x) =>
			{
				x.WithMicrosoftLogging(log =>
				{
					log.AddConsole(LogLevel.Debug);
				})
				.WithDictionaryHandle();
			};
			services.AddOcelot(Configuration, settings);
		}

		public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			await app.UseOcelot();
		}
	}
}
