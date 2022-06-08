using easyCloud.Quote.Domain.Repositories;
using easyCloud.Quote.Domain.Services;
using easyCloud.Quote.Domain.Services.Communication;
using easyCloud.Shared.Domain.Repositories;

namespace easyCloud.Quote.Services;

public class QuoteService : IQuoteService
{
    private readonly IQuoteRepository _quoteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public QuoteService(IQuoteRepository quoteRepository, IUnitOfWork unitOfWork)
    {
        _quoteRepository = quoteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Domain.Models.Quote>> ListAsync()
    {
        return await _quoteRepository.ListAsync();
    }

    public async Task<QuoteResponse> SaveAsync(Domain.Models.Quote quote)
    {
        try
        {
            await _quoteRepository.AddAsync(quote);
            await _unitOfWork.CompleteAsync();

            return new QuoteResponse(quote);
        }
        catch (Exception e)
        {
            return new QuoteResponse($"An error occurred while saving the quote: {e.Message}");
        }
    }

    public async Task<QuoteResponse> UpdateAsync(int quoteId, Domain.Models.Quote quote)
    {
        var existingQuote = await _quoteRepository.FindByIdAsync(quoteId);

        if (existingQuote == null)
            return new QuoteResponse("Quote not found");
        
        existingQuote.Date = quote.Date;
        existingQuote.Description = quote.Description;
        existingQuote.Title = quote.Title;
        existingQuote.Price = quote.Price;
        existingQuote.CloudService = quote.CloudService;
        
        try
        {
            _quoteRepository.Update(existingQuote);
            await _unitOfWork.CompleteAsync();

            return new QuoteResponse(existingQuote);
        }
        catch (Exception e)
        {
            return new QuoteResponse($"An error occurred while updating the quote: {e.Message}");
        }
    }

    public async Task<QuoteResponse> DeleteAsync(int quoteId)
    {
        var existingQuote = await _quoteRepository.FindByIdAsync(quoteId);

        if (existingQuote == null)
            return new QuoteResponse("Quote not found");
            
        try
        {
            _quoteRepository.Remove(existingQuote);
            await _unitOfWork.CompleteAsync();

            return new QuoteResponse(existingQuote);
        }
        catch (Exception e)
        {
            return new QuoteResponse($"An error occurred while deleting the quote: {e.Message}");
        }
    }
}