using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlikkBaiscReplica.Models
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
