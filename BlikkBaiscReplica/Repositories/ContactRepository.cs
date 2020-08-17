using System.Collections.Generic;
using System.Threading.Tasks;
using BlikkBaiscReplica.Data;
using BlikkBaiscReplica.Models;
using Microsoft.EntityFrameworkCore;

namespace BlikkBaiscReplica.Repositories
{
    public class ContactRepository : IRepository<Contact>
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAll()
        {
            var contacts = await _context.Contacts.AsNoTracking()
                .Include(x => x.VisitingAddress)
                .ToListAsync();
            return contacts;
        }

        public async Task<Contact> Get(int id)
        {
            var contact = await _context.Contacts.AsNoTracking().Include(x => x.VisitingAddress)
                .FirstOrDefaultAsync(x => x.Id == id);
            return contact ?? null;
        }

        public async Task<Contact> Update(Contact entity)
        {
            var user = await Get(entity.Id);
            entity.ApplicationUserId = user.ApplicationUserId;
            _context.Contacts.Update(entity);
            var result = await _context.SaveChangesAsync();

            //TODO return other than null if update failed
            return result > 0 ? entity : null;
        }

        public async Task<Contact> Add(Contact entity)
        {
            await _context.Contacts.AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            //TODO return other than null if add failed
            return result > 0 ? entity : null;
        }

        public async Task<Contact> Delete(Contact contact)
        {
            _context.Contacts.Remove(contact);
            var result = await _context.SaveChangesAsync();

            //TODO return other than null if delete failed
            return result > 0 ? contact : null;
        }
    }
}