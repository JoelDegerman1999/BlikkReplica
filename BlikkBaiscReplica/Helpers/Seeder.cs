using System.Collections.Generic;
using BlikkBasicReplica.API.Data;
using BlikkBasicReplica.API.Models;
using Newtonsoft.Json;

namespace BlikkBasicReplica.API.Helpers
{
    public static class Seeder
    {
        public static void SeedUsers(ApplicationDbContext context)
        {
            var contactData = System.IO.File.ReadAllText("generated.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(contactData);

            foreach (var contact in contacts)
            {
                context.Contacts.Add(contact);
            }

            context.SaveChanges();
        }
    }
}
