using System;
using Newtonsoft.Json;

namespace BlikkBasicReplica.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }

        [JsonIgnore]
        public string ApplicationUserId { get; set; }
    }
}
