using easyCloud.Provider.Domain.Repositories;
using easyCloud.Record.Domain.Repositories;
using easyCloud.Shared.Persistence.Contexts;
using easyCloud.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace easyCloud.Provider.Persistence.Repositories;

public class ProviderRepository : BaseRepository , IProviderRepository
{
    public ProviderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Domain.Models.Provider>> ListAsync()
    {
        return await _context.Providers
            .ToListAsync();       
    }

    public async Task AddAsync(Domain.Models.Provider provider)
    {
        await _context.Providers.AddAsync(provider);
    }

    public Task<Domain.Models.Provider> FindByIdAsync(int providerId)
    {
        throw new NotImplementedException();
    }

    public void Update(Domain.Models.Provider provider)
    {
        _context.Providers.Update(provider);
    }

    public void Remove(Domain.Models.Provider provider)
    {
        _context.Providers.Remove(provider);
    }
}