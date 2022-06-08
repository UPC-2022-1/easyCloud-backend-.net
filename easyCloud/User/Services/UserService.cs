using AutoMapper;
using easyCloud.Shared.Domain.Repositories;
using easyCloud.User.Domain.Repositories;
using easyCloud.User.Domain.Services;
using easyCloud.User.Domain.Services.Communication;

namespace easyCloud.User.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<Domain.Models.User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<UserResponse> FindByEmailAsync(string email)
    {
        var existingUser = await _userRepository.FindByEmailAsync(email);

        if (existingUser == null)
            return new UserResponse("Invalid email");

        return new UserResponse(existingUser);
    }

    public async Task<UserResponse> SaveAsync(Domain.Models.User user)
    {
        var existingUserWithEmail = _userRepository.ExistByEmail(user.Email);

        if (existingUserWithEmail)
            return new UserResponse("Invalid Email");

        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return new UserResponse(user);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while saving the user: {e.Message}");
        }
    }

    public async Task<UserResponse> UpdateAsync(int userId, Domain.Models.User user)
    {
        var existingUser = await _userRepository.FindByIdAsync(userId);

        if (existingUser == null)
            return new UserResponse("User not found");

        existingUser.Email = user.Email;
        existingUser.Name = user.Name;
        existingUser.Phone = user.Phone;
        existingUser.Password = user.Password;

        try
        {
            _userRepository.Update(existingUser);
            await _unitOfWork.CompleteAsync();

            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while updating the user: {e.Message}");
        }
    }

    public async Task<UserResponse> DeleteAsync(int userId)
    {
        var existingUser = await _userRepository.FindByIdAsync(userId);

        if (existingUser == null)
            return new UserResponse("User not found");
            
        try
        {
            _userRepository.Remove(existingUser);
            await _unitOfWork.CompleteAsync();

            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while deleting the user: {e.Message}");
        }
    }
}