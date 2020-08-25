using Microsoft.EntityFrameworkCore;

namespace BlikkBasicReplica.API.Models
{
    [Owned]
    public class Address
    {
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
