using System.Net;

namespace Carbon.Interface.Exceptions;

public class CarbonInterfaceException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    
    public CarbonInterfaceException(string message, HttpStatusCode status) : base(message)
    {
        StatusCode = status;
    }
}