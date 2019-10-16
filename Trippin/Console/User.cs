using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace console
{
    public class User
    {
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }
        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("LastName")]
        public string LastName { get; set; }
        [JsonPropertyName("Email")]
        public string Email { get; set; }
        [JsonPropertyName("Address")]
        public string Address { get; set; }
        [JsonPropertyName("CityName")]
        public string CityName { get; set; }
        [JsonPropertyName("Country")]
        public string Country { get; set; }
    }

    public class Users
    {
        [JsonPropertyName("results")]
        public List<User> Results { get; set; }
    }
}
