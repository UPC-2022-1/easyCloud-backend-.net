using AutoMapper;
using easyCloud.Security.Authorization.Handlers.Interfaces;
using easyCloud.Security.Domain.Services.Communication;
using easyCloud.Security.Exceptions;
using easyCloud.Shared.Domain.Repositories;
using easyCloud.User.Domain.Repositories;
using easyCloud.User.Domain.Services;
using easyCloud.User.Domain.Services.Communication;
using BCryptNet = BCrypt.Net.BCrypt;

namespace easyCloud.User.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtHandler _jwtHandler;

public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IJwtHandler jwtHandler)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
    }

public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
{
    var user = await _userRepository.FindByEmailAsync(request.Email);
    Console.WriteLine($"Request: {request.Email}, {request.Password}");
    Console.WriteLine($"User: {user.Id}, {user.Email}, {user.Name}, {user.Phone}, {user.Password}");
        
    // Validate
    if (user == null || !BCryptNet.Verify(request.Password, user.Password))
    {
        Console.WriteLine("Authentication Error");
        throw new AppException("Username of password is incorrect");
    }
        
    Console.WriteLine("Authentication successful. About to generate token");
        
    //Authentication successful
    var response = _mapper.Map<AuthenticateResponse>(user);
    Console.WriteLine($"Response: {response.Id}, {response.Id}, {response.Name}, {response.Email}");
    response.Token = _jwtHandler.GenerateToken(user);
    Console.WriteLine($"Generated Token is {response.Token}");
    return response;
}

public async Task<IEnumerable<Domain.Models.User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<Domain.Models.User> GetByIdAsync(int userId)
    {
        var user = await _userRepository.FindByIdAsync(userId);
        if (user == null) throw new KeyNotFoundException("User not found.");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        // Validate 
        if (_userRepository.ExistByEmail(request.Email))
            throw new AppException($"Username '{request.Email}' is already taken");
        
        // Map Request to User Entity
        var user = _mapper.Map<Domain.Models.User>(request);
        
        // Hash Password
        user.Password = BCryptNet.HashPassword(request.Password);
        
        // Save User

        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public async Task UpdateAsync(int userId, UpdateRequest user)
    {
        var existingUser = GetById(userId);
        
        if (!string.IsNullOrEmpty(user.Password))
            existingUser.Password = BCryptNet.HashPassword(user.Password);
            
        _mapper.Map(user, existingUser);
            
        try
        {
            _userRepository.Update(existingUser);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }

    public async Task DeleteAsync(int userId)
    {
        var user = GetById(userId);
            
        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
    
    private Domain.Models.User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found.");
        return user;
    }
}