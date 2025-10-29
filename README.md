This is a simple MCP server with weather information, written on C#. It consists of:  
`get_weather(cityName)` tool: returns brief weather information  
`weather://{cityName}` resource: return same information as a tool.

### How to run
To build, simply write in project directory:
`dotnet build`  
  
To run, as expected:  
`dotnet run`  

You can use mcp-inspector to test functionality. In project directory:  
npx @modelcontextprotocol/inspector dotnet run
