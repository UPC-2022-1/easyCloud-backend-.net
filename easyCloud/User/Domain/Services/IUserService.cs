using easyCloud.Security.Domain.Services.Communication;
using easyCloud.User.Domain.Services.Communication;

namespace easyCloud.User.Domain.Services;

public interface IUserService
{
    public Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    Task<IEnumerable<Models.User>> ListAsync();
    Task<Domain.Models.User> GetByIdAsync(int userId);
    Task RegisterAsync(RegisterRequest request);
    Task UpdateAsync(int userId, UpdateRequest user);
    Task DeleteAsync(int userId);
}