using AccessCorpUsers.Application.Entities;
using AccessCorpUsers.Domain.Entities;
using AutoMapper;

namespace AccessCorpUsers.Application.Configuration
{
    public  class AutomapperConfig : Profile
    {
        public AutomapperConfig() 
        {
            CreateMap<Administrator, AdministratorVM>().ReverseMap();
            CreateMap<Doorman, DoormanVM>().ReverseMap();
        }
    }
}
