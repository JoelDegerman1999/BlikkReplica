using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BlikkBasicReplica.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Order> Orders { get; set; }

        public ApplicationUser()
        {
            Contacts = new List<Contact>();
            Orders =  new List<Order>();
        }
    }
}
