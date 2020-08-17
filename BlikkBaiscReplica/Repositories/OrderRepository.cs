using System.Collections.Generic;
using System.Threading.Tasks;
using BlikkBaiscReplica.Data;
using BlikkBaiscReplica.Models;
using Microsoft.EntityFrameworkCore;

namespace BlikkBaiscReplica.Repositories
{
    public class OrderRepository: IRepository<Order>
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAll()
        {
            var orders = await _context.Orders.AsNoTracking().ToListAsync();

            return orders;
        }

        public async Task<Order> Get(int id)
        {
            var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return order ?? null;
        }

        public async Task<Order> Update(Order entity)
        {
            var user = await Get(entity.Id);
            entity.ApplicationUserId = user.ApplicationUserId;
            _context.Orders.Update(entity);
            var result = await _context.SaveChangesAsync();

            //TODO return other than null if update failed
            return result > 0 ? entity : null;

        }

        public async Task<Order> Add(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            //TODO return other than null if add failed
            return result > 0 ? entity : null;
        }

        public async Task<Order> Delete(Order order)
        {
            _context.Orders.Remove(order);
            var result = await _context.SaveChangesAsync();

            //TODO return other than null if delete failed
            return result > 0 ? order : null;
        }
    }
}
