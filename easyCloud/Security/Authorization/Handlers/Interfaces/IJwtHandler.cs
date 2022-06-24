namespace easyCloud.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    string GenerateToken(User.Domain.Models.User user);
    int? ValidateToken(string token);
    
}