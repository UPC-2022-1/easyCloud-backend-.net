using easyCloud.Provider.Domain.Repositories;
using easyCloud.Provider.Domain.Services;
using easyCloud.Provider.Domain.Services.Communication;
using easyCloud.Shared.Domain.Repositories;

namespace easyCloud.Provider.Services;

public class ProviderService : IProviderService
{
    private readonly IProviderRepository _providerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProviderService(IProviderRepository providerRepository, IUnitOfWork unitOfWork)
    {
        _providerRepository = providerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Domain.Models.Provider>> ListAsync()
    {
        return await _providerRepository.ListAsync();
    }

    public async Task<ProviderResponse> SaveAsync(Domain.Models.Provider provider)
    {
        try
        {
            await _providerRepository.AddAsync(provider);
            await _unitOfWork.CompleteAsync();

            return new ProviderResponse(provider);
        }
        catch (Exception e)
        {
            return new ProviderResponse($"An error occurred while saving the provider: {e.Message}");
        }
    }

    public async Task<ProviderResponse> UpdateAsync(int providerId, Domain.Models.Provider provider)
    {
        var existingProvider = await _providerRepository.FindByIdAsync(providerId);

        if (existingProvider == null)
            return new ProviderResponse("User not found");
        
        existingProvider.Name = provider.Name;
        existingProvider.Website = provider.Website;

        try
        {
            _providerRepository.Update(existingProvider);
            await _unitOfWork.CompleteAsync();

            return new ProviderResponse(existingProvider);
        }
        catch (Exception e)
        {
            return new ProviderResponse($"An error occurred while updating the provider: {e.Message}");
        }
    }

    public async Task<ProviderResponse> DeleteAsync(int providerId)
    {
        var existingProvider = await _providerRepository.FindByIdAsync(providerId);

        if (existingProvider == null)
            return new ProviderResponse("User not found");
            
        try
        {
            _providerRepository.Remove(existingProvider);
            await _unitOfWork.CompleteAsync();

            return new ProviderResponse(existingProvider);
        }
        catch (Exception e)
        {
            return new ProviderResponse($"An error occurred while deleting the user: {e.Message}");
        }
    }
}