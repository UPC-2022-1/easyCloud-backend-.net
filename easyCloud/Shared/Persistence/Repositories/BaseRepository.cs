using easyCloud.Shared.Persistence.Contexts;

namespace easyCloud.Shared.Persistence.Repositories;

public abstract class BaseRepository
{
    protected readonly AppDbContext _context;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}