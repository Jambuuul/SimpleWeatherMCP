using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using SimpleMCP;
using System.ComponentModel;
using System.Net.Http.Headers;

Console.WriteLine("MCP server started!");

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()  // to use stdin/stdout for communication
    .WithResources<WeatherResource>()
    .WithToolsFromAssembly()     // auto registraion of MCP tools
    .WithResourcesFromAssembly();
    

await builder.Build()
    .RunAsync();


