using AutoMapper;
using easyCloud.Provider.Resources;
using easyCloud.Quote.Resources;
using easyCloud.Record.Resources;
using easyCloud.User.Resources;

namespace easyCloud.Shared.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Quote.Domain.Models.Quote, QuoteResource>();
        CreateMap<Provider.Domain.Models.Provider, ProviderResource>();
        CreateMap<Record.Domain.Models.Record, RecordResource>();
        CreateMap<User.Domain.Models.User, UserResource>();
    }
}