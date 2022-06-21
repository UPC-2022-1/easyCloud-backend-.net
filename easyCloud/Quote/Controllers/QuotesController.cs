using AutoMapper;
using easyCloud.Quote.Domain.Services;
using easyCloud.Quote.Resources;
using easyCloud.Record.Resources;
using easyCloud.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace easyCloud.Quote.Controllers;
[Produces("application/json")]
[ApiController]
[Route("/api/v1/[controller]")]

public class QuotesController: ControllerBase
{
    private readonly IQuoteService _quoteService;
    private readonly IMapper _mapper;
    

    public QuotesController(IQuoteService quoteService, IMapper mapper)
    {
        _quoteService = quoteService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("test")]
    public async Task<IEnumerable<QuoteResource>> GetAllAsync()
    {
        var quotes = await _quoteService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Domain.Models.Quote>, IEnumerable<QuoteResource>>(quotes);

        return resources;
    }
    
   
    [HttpPost("add/{userId}")]
    public async Task<IActionResult> PostAsync([FromBody] SaveQuoteResource resource, int userId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var quote = _mapper.Map<SaveQuoteResource, Domain.Models.Quote>(resource);
        quote.UserId = userId;
        var result = await _quoteService.SaveAsync(quote);

        if (!result.Success)
            return BadRequest(result.Message);

        var quoteResource = _mapper.Map<Domain.Models.Quote, QuoteResource>(result.Resource);

        return Ok(quoteResource);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _quoteService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var quoteResource = _mapper.Map<Domain.Models.Quote, QuoteResource>(result.Resource);

        return Ok(quoteResource);
    }
}