using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Shared.Logger
{
	public static class SerilogExtensions
	{
		private const string SeqUrlKey = "http://seq:5341";
		public static IHostBuilder UseCentralizedSerilog(this IHostBuilder hostBuilder, string applicationName)
		{
			LoggerSetup.Configure(applicationName, SeqUrlKey);

			return hostBuilder.UseSerilog();
		}
	}
}