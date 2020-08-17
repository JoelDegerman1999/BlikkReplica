using System.Collections.Generic;

namespace BlikkBasicReplica.Models.ViewModels
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
