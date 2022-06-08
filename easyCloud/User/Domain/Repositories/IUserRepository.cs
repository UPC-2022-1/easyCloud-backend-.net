namespace easyCloud.User.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<Models.User>> ListAsync();
    Task AddAsync(Models.User user);
    Task<Models.User> FindByIdAsync(int userId);
    Task<Models.User> FindByEmailAsync(string email);
    public bool ExistByEmail(string email);
    void Update(Models.User user);
    void Remove(Models.User user);
}