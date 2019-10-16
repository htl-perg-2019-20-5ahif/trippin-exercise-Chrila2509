
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace console
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("https://services.odata.org/TripPinRESTierService/(S(hnrgtj2g2bofx0noffhzhg3e))/") };
               
        static async Task Main(string[] args)
        {
            var users = JsonSerializer.Deserialize<User[]>(await File.ReadAllTextAsync("users.json"));

            foreach (var user in users)
            {
                if (!await IsExisting(user))
                {
                    Console.WriteLine("Adding user");
                    await AddUser(user);
                }
            }
        }

        private static async Task<bool> AddUser(User user)
        {
            {
                var newUser = new
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Emails = new[] {
                        user.Email
                    },
                    AddressInfo = new[] {
                        new
                        {
                            Address = user.Address,
                            City = new
                            {
                                Name = user.CityName,
                                CountryRegion = user.Country,
                                Region = "unknown"
                            }
                        }
                    }
                };
                var retObject = new StringContent(JsonSerializer.Serialize(newUser));
                return (await client.PostAsync("Users", retObject)).IsSuccessStatusCode;
            }
        }

        private static async Task<bool> IsExisting(User user)
        {
            return (await client.GetAsync("Users('user.UserName')")).IsSuccessStatusCode;
        }
    }
}
