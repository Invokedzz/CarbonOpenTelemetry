using System.Net;

namespace Carbon.Domain.Exceptions
{
    public class UnauthorizedException(string message) : DomainException(message)
    {
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Unauthorized;
    }
    
    // Usuário logado, mas o perfil dele não pode fazer isso (HTTP 403)
    public class ForbiddenException(string message) : DomainException(message)
    {
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Forbidden;
    }

    public class NotFoundException(string message) : DomainException(message)
    {
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;
    }

    public class BadRequestException(string message) : DomainException(message)
    {
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }

    public class InternalServerError(string message) : DomainException(message)
    {
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    }

    public class DomainException : Exception
    {
        public virtual HttpStatusCode StatusCode { get; set; }
        protected DomainException(string message) : base(message) {}
    }
}