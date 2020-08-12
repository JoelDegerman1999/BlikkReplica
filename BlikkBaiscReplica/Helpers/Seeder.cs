using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlikkBaiscReplica.Data;
using BlikkBaiscReplica.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BlikkBaiscReplica.Helpers
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
