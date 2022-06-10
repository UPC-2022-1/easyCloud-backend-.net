using AutoMapper;
using easyCloud.Provider.Domain.Services;
using easyCloud.Provider.Resources;
using easyCloud.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace easyCloud.Provider.Controllers;
[Produces("application/json")]
[ApiController]
[Route("/api/v1/[controller]")]
public class ProvidersController: ControllerBase
{
    private readonly IProviderService _providerService;
    private readonly IMapper _mapper;
    

    public ProvidersController(IProviderService providerService, IMapper mapper)
    {
        _providerService = providerService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProviderResource>> GetAllAsync()
    {
        var providers = await _providerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Domain.Models.Provider>, IEnumerable<ProviderResource>>(providers);

        return resources;
    }
        
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveProviderResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var provider = _mapper.Map<SaveProviderResource, Domain.Models.Provider>(resource);

        var result = await _providerService.SaveAsync(provider);

        if (!result.Success)
            return BadRequest(result.Message);

        var providerResource = _mapper.Map<Domain.Models.Provider, ProviderResource>(result.Resource);

        return Ok(providerResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProviderResource resource)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var provider = _mapper.Map<SaveProviderResource, Domain.Models.Provider>(resource);

        var result = await _providerService.UpdateAsync(id, provider);

        if (!result.Success)
            return BadRequest(result.Message);

        var providerResource = _mapper.Map<Domain.Models.Provider, ProviderResource>(result.Resource);

        return Ok(providerResource);
    }
        
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _providerService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var providerResource = _mapper.Map<Domain.Models.Provider, ProviderResource>(result.Resource);

        return Ok(providerResource);
    }
}