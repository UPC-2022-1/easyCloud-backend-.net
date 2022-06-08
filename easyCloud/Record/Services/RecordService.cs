using easyCloud.Record.Domain.Repositories;
using easyCloud.Record.Domain.Services;
using easyCloud.Record.Domain.Services.Communication;
using easyCloud.Shared.Domain.Repositories;

namespace easyCloud.Record.Services;

public class RecordService : IRecordService
{
    private readonly IRecordRepository _recordRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RecordService(IRecordRepository recordRepository, IUnitOfWork unitOfWork)
    {
        _recordRepository = recordRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Domain.Models.Record>> ListAsync()
    {
        return await _recordRepository.ListAsync();
    }

    public async Task<RecordResponse> SaveAsync(Domain.Models.Record record)
    {
        try
        {
            await _recordRepository.AddAsync(record);
            await _unitOfWork.CompleteAsync();

            return new RecordResponse(record);
        }
        catch (Exception e)
        {
            return new RecordResponse($"An error occurred while saving the record: {e.Message}");
        }
    }

    public async Task<RecordResponse> UpdateAsync(int recordId, Domain.Models.Record record)
    {
        var existingRecord = await _recordRepository.FindByIdAsync(recordId);

        if (existingRecord == null)
            return new RecordResponse("Record not found");
        
        existingRecord.UserId = record.UserId;
        existingRecord.ProviderId = record.ProviderId;
        existingRecord.QuoteId = record.QuoteId;
        
        try
        {
            _recordRepository.Update(existingRecord);
            await _unitOfWork.CompleteAsync();

            return new RecordResponse(existingRecord);
        }
        catch (Exception e)
        {
            return new RecordResponse($"An error occurred while updating the record: {e.Message}");
        }
    }

    public async Task<RecordResponse> DeleteAsync(int recordId)
    {
        var existingRecord = await _recordRepository.FindByIdAsync(recordId);

        if (existingRecord == null)
            return new RecordResponse("Record not found");
            
        try
        {
            _recordRepository.Remove(existingRecord);
            await _unitOfWork.CompleteAsync();

            return new RecordResponse(existingRecord);
        }
        catch (Exception e)
        {
            return new RecordResponse($"An error occurred while deleting the record: {e.Message}");
        }
    }
}