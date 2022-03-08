using Quotation.API;

var configuration = GetConfiguration();

Log.Logger = CreateSerilogLogger(configuration);

try
{
    Log.Information("Configuring HostBuilder ({ApplicationContext})...", Program.AppName);
    var hostBuilder = CreateHostBuilder(configuration, args);

    Log.Information("Starting host ({ApplicationContext})...", Program.AppName);
    hostBuilder.Build().Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", Program.AppName);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(
        webBuilder => webBuilder.CaptureStartupErrors(false)
                    .ConfigureAppConfiguration((hostContext, builder) =>
                    {
                        builder.AddConfiguration(configuration);

                        if (hostContext.HostingEnvironment.IsDevelopment())
                        {
                            builder.AddUserSecrets<Program>();
                        }
                    })
                    .UseStartup<Startup>()
                    .UseContentRoot(Directory.GetCurrentDirectory())
        );

Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    var seqServerUrl = configuration["Serilog:SeqServerUrl"];
    var logstashUrl = configuration["Serilog:LogstashgUrl"];
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", Program.AppName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    var config = builder.Build();

    return builder.Build();
}

(int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
{
    var grpcPort = config.GetValue("GRPC_PORT", 5001);
    var port = config.GetValue("PORT", 80);
    return (port, grpcPort);
}

public partial class Program
{

    public static string Namespace = typeof(Startup).Namespace;
    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}
