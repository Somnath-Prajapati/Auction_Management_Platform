using System.Security.Claims;


namespace AuctionManagementSystem.Api.Services
{
    public class LoggedInUserService: ILoggedInUserService
    {
        public string UserId { get; }

        public LoggedInUserService(IHttpContextAccessor contextAccessor)
        {
            UserId = contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        }
    }
}
