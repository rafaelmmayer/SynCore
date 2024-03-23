namespace SynCore.Api.Common.Exceptions;

public class AppException : Exception
{
    public int Status { get; }
    
    public AppException(int status, string message) : base(message)
    {
        Status = status;
    }
}