namespace easyCloud.Record.Domain.Repositories;

public interface IRecordRepository
{
    Task<IEnumerable<Models.Record>> ListAsync();
    Task AddAsync(Models.Record record);
    Task<Models.Record> FindByIdAsync(int recordId);
    void Update(Models.Record record);
    void Remove(Models.Record record);
}