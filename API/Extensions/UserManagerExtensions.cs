using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {

        public static async Task<AppUser> FindUserByClaimsWithAddress(this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.Include(x=>x.Adress)
                .SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser> FindByEmailFromClaims(this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            return await userManager.Users.SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        }


    }
}
