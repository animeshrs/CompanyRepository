using Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Animesh",
                        UserName = "animesh",
                        Email = "animesh@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$wOrd");
                }
            }

            if (context.Companies.Any())
                return;

            var companies = new List<Company>
                {
                    new Company
                    {
                        Exchange = "NASDAQ",
                        ISIN = "US0378331005",
                        Name = "Apple Inc.",
                        Ticker = "AAPL",
                        Website = "http://www.apple.com"
                    },
                    new Company
                    {
                        Exchange = "Pink Sheets",
                        ISIN = "US1104193065",
                        Name = "British Airways Plc",
                        Ticker = "BAIRY",
                        Website = ""
                    },
                    new Company
                    {
                        Exchange = "Euronext Amsterdam",
                        ISIN = "NL0000009165",
                        Name = "Heineken NV",
                        Ticker = "HEIA",
                        Website = ""
                    },
                    new Company
                    {
                        Exchange = "Tokyo Stock Exchange",
                        ISIN = "JP3866800000",
                        Name = "Panasonic Corp",
                        Ticker = "6752",
                        Website = "http://www.panasonic.co.jp"
                    },
                    new Company
                    {
                        Exchange = "Deutsche Börse",
                        ISIN = "DE000PAH0038",
                        Name = "Porsche Automobil",
                        Ticker = "PAH3",
                        Website = "https://www.porsche.com"
                    }
                };

            await context.Companies.AddRangeAsync(companies);
            await context.SaveChangesAsync();
        }
    }
}
