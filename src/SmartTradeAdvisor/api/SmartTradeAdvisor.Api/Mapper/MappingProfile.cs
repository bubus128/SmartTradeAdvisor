using AutoMapper;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Api.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MarketIndex, MarketIndexDto>().ReverseMap()
            .ForMember(dest => dest.MarketIndexValues, opt => opt.NullSubstitute(new List<MarketIndexValue>()));
        CreateMap<MarketIndexValue, MarketIndexValueDto>().ReverseMap();
    }
}
