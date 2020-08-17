using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlikkBaiscReplica.Models.ViewModels
{
    public class ApplicationUserVm
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Order> Orders { get; set; }

        public ApplicationUserVm()
        {
            Contacts = new List<Contact>();
            Orders = new List<Order>();
        }
    }
}
