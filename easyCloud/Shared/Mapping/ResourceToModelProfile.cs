using AutoMapper;
using easyCloud.Provider.Resources;
using easyCloud.Quote.Resources;
using easyCloud.Record.Resources;
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
    }
}