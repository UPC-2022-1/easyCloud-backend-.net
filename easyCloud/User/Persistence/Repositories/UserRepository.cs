using easyCloud.Shared.Persistence.Contexts;
using easyCloud.Shared.Persistence.Repositories;
using easyCloud.User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace easyCloud.User.Persistence.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Domain.Models.User>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(Domain.Models.User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<Domain.Models.User> FindByIdAsync(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(p => p.Id == userId);
    }
    
    public async Task<Domain.Models.User> FindByEmailAsync(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(p => p.Email == email);
    }

    public Domain.Models.User FindById(int userId)
    {
        return _context.Users.Find(userId);
    }

    public bool ExistByEmail(string email)
    {
        return _context.Users.Any(p => p.Email == email);
    }

    public void Update(Domain.Models.User user)
    {
        _context.Users.Update(user);
    }

    public void Remove(Domain.Models.User user)
    {
        _context.Users.Remove(user);
    }
}