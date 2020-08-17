using System.Collections.Generic;
using BlikkBasicReplica.Data;
using BlikkBasicReplica.Models;
using Newtonsoft.Json;

namespace BlikkBasicReplica.Helpers
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
