using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleMCP
{
    public record ErrorInfo(int Code, string Message);

    /// <summary>
    /// Structure of our response
    /// </summary>
    public class JsonResponse
    {
        public bool Success { get; init; }
        public Dictionary<string, object>? Data { get; init; }
        public ErrorInfo? Error { get; init; } 
    }
}
