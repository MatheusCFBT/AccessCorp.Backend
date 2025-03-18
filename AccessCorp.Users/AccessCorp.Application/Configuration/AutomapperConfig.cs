using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Domain.Entities;
using AutoMapper;

namespace AccessCorpUsers.Application.Configuration
{
    public  class AutomapperConfig : Profile
    {
        public AutomapperConfig() 
        {
            CreateMap<Administrator, AdministratorVM>()
            .ForMember(dest => dest.Residents, opt => opt.MapFrom(src => (IEnumerable<Resident>?)null))
            .ForMember(dest => dest.Doormans, opt => opt.MapFrom(src => (IEnumerable<Doorman>?)null));

            CreateMap<AdministratorVM, Administrator>()
                .ForMember(dest => dest.Residents, opt => opt.Ignore())
                .ForMember(dest => dest.Doormans, opt => opt.Ignore());


            CreateMap<Doorman, DoormanVM>().ReverseMap();
        }
    }
}
