using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using AuctionManagementSystem.Application.Contracts.AuditTrail;

namespace AuctionManagementSystem.Identity.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId { get; }
        public string Username { get; }
        public string RoleName { get; }

        public int RoleId { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            Console.WriteLine("==== JWT Claims Received ====");
            foreach (var claim in user?.Claims ?? Enumerable.Empty<Claim>())
            {
                Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
            }


            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdClaim, out int uid))
                UserId = uid;

            Username = user.FindFirstValue(ClaimTypes.Name) ?? "Unknown";

            var roleIdClaim = user?.Claims.FirstOrDefault(c => c.Type == "role_id")?.Value;
            if (int.TryParse(roleIdClaim, out int rid))
                RoleId = rid;

            RoleName = user.FindFirstValue("role_name") ?? user.FindFirstValue(ClaimTypes.Role) ?? "Unknown";
        }


    }
}
