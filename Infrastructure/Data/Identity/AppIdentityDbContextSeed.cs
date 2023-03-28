using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Identity
{
    public  class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Marouane",
                    Email = "marouane@test.com",
                    UserName = "marouane@test.com",
                    Adress = new Address
                    {
                        FirstName = "mar",
                        LastName = "Ag",
                        Street = "street 10",
                        City = "salé",
                        State = "sl",
                        ZipCode = "4565"
                    }

                };

                 await userManager.CreateAsync(user,"Pa$$w0rd");  
            }
        }
    }
}
