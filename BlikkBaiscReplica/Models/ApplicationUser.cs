using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlikkBaiscReplica.Models
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
