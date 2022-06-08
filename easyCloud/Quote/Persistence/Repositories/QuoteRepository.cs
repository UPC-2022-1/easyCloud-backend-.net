using easyCloud.Quote.Domain.Repositories;
using easyCloud.Shared.Persistence.Contexts;
using easyCloud.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace easyCloud.Quote.Persistence.Repositories;

public class QuoteRepository :BaseRepository, IQuoteRepository
{
    public QuoteRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Domain.Models.Quote>> ListAsync()
    {
        return await _context.Quotes
            .ToListAsync();
    }

    public async Task AddAsync(Domain.Models.Quote quote)
    {
        await _context.Quotes.AddAsync(quote);
    }

    public async Task<Domain.Models.Quote> FindByIdAsync(int quoteId)
    {
        return await _context.Quotes
            .FirstOrDefaultAsync(p => p.Id == quoteId);
    }

    public void Update(Domain.Models.Quote quote)
    {
        _context.Quotes.Update(quote);
    }
    

    public void Remove(Domain.Models.Quote quote)
    {
        _context.Quotes.Remove(quote);
    }

    public async Task<Domain.Models.Quote> FindByDateAsync(string date)
    {
        return await _context.Quotes.SingleOrDefaultAsync(p => p.Date == date);
    }

    public async Task<Domain.Models.Quote> FindByTitleAsync(string title)
    {
        return await _context.Quotes.SingleOrDefaultAsync(p => p.Title == title);
    }
}