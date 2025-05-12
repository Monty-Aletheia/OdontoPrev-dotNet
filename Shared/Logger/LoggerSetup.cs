using Serilog;
using Serilog.Events;

namespace Shared.Logger
{
	public static class LoggerSetup
	{
		public static void Configure(string applicationName, string seqUrl)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.MinimumLevel.Override("System", LogEventLevel.Warning)
				.Enrich.FromLogContext()
				.Enrich.WithMachineName()
				.Enrich.WithThreadId()
				.Enrich.WithProperty("Application", applicationName)
				.WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Application} | {Message:lj}{NewLine}{Exception}")
				.WriteTo.Seq(seqUrl)
				.CreateLogger();
		}
	}
}