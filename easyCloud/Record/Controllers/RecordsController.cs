using AutoMapper;
using easyCloud.Quote.Resources;
using easyCloud.Record.Domain.Services;
using easyCloud.Record.Resources;
using easyCloud.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace easyCloud.Record.Controllers;
[Produces("application/json")]
[ApiController]
[Route("/api/v1/[controller]")]

public class RecordsController: ControllerBase
{
    private readonly IRecordService _recordService;
    private readonly IMapper _mapper;
    

    public RecordsController(IRecordService recordService, IMapper mapper)
    {
        _recordService = recordService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<RecordResource>> GetAllAsync()
    {
        var records = await _recordService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Domain.Models.Record>, IEnumerable<RecordResource>>(records);

        return resources;
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveRecordResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var record = _mapper.Map<SaveRecordResource, Domain.Models.Record>(resource);

        var result = await _recordService.SaveAsync(record);

        if (!result.Success)
            return BadRequest(result.Message);

        var recordResource = _mapper.Map<Domain.Models.Record, RecordResource>(result.Resource);

        return Ok(recordResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRecordResource resource)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var record = _mapper.Map<SaveRecordResource, Domain.Models.Record>(resource);

        var result = await _recordService.UpdateAsync(id, record);

        if (!result.Success)
            return BadRequest(result.Message);

        var recordResource = _mapper.Map<Domain.Models.Record, RecordResource>(result.Resource);

        return Ok(recordResource);
    }
        
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _recordService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var recordResource = _mapper.Map<Domain.Models.Record, RecordResource>(result.Resource);

        return Ok(recordResource);
    }
}