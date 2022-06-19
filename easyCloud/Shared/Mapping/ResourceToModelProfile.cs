using AutoMapper;
using easyCloud.Provider.Resources;
using easyCloud.Quote.Resources;
using easyCloud.Record.Resources;
using easyCloud.Security.Domain.Services.Communication;
using easyCloud.User.Resources;

namespace easyCloud.Shared.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveRecordResource, Record.Domain.Models.Record>();
        CreateMap<SaveQuoteResource, Quote.Domain.Models.Quote>();
        CreateMap<SaveProviderResource, Provider.Domain.Models.Provider>();
        CreateMap<SaveUserResource, User.Domain.Models.User>();
        CreateMap<RegisterRequest, User.Domain.Models.User>();
        CreateMap<UpdateRequest, User.Domain.Models.User>()
            .ForAllMembers(options => options.Condition(
                (source, target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && 
                        string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }));
    }
}