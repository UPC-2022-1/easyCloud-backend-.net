using easyCloud.Record.Domain.Repositories;
using easyCloud.Shared.Persistence.Contexts;
using easyCloud.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace easyCloud.Record.Persistence.Repositories;

public class RecordRepository : BaseRepository, IRecordRepository
{
    public RecordRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Domain.Models.Record>> ListAsync()
    {
        return await _context.Records
            .ToListAsync();
    }

    public async Task AddAsync(Domain.Models.Record record)
    {
        await _context.Records.AddAsync(record);
    }

    public async Task<Domain.Models.Record> FindByIdAsync(int recordId)
    {
        return await _context.Records
            .FirstOrDefaultAsync(p => p.Id == recordId);
    }

    public void Update(Domain.Models.Record record)
    {
        _context.Records.Update(record);
    }

    public void Remove(Domain.Models.Record record)
    {
        _context.Records.Remove(record);
    }
}