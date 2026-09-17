using System.Net;

namespace Carbon.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;
        public NotFoundException(string message) : base(message) {}
    }

    public class BadRequestException : DomainException
    {
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public BadRequestException(string message) : base(message) {}
    }

    public class InternalServerError : DomainException
    {
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public InternalServerError(string message) : base(message) {}
    }

    public class DomainException : Exception
    {
        public virtual HttpStatusCode StatusCode { get; set; }
        protected DomainException(string message) : base(message) {}
    }
}