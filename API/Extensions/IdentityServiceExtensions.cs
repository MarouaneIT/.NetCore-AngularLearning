using Core.Entities.Identity;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration config)
        {

            services.AddDbContext<AppIdentityDbContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentityCore<AppUser>(opt =>
            {
                //you can add options here

            })
             .AddEntityFrameworkStores<AppIdentityDbContext>()
             .AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:key"])),
                        ValidateIssuer = true,
                        ValidIssuer = config["Token:Issuer"],
                        ValidateAudience = false

                    };
                });


            services.AddAuthorization();


            return services;
        }
    }
}
