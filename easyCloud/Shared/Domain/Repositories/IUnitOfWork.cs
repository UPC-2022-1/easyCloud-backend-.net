namespace easyCloud.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}