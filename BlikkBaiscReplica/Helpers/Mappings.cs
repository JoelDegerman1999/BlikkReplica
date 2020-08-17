using AutoMapper;
using BlikkBasicReplica.Models;
using BlikkBasicReplica.Models.ViewModels;

namespace BlikkBasicReplica.Helpers
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<ApplicationUser, ApplicationUserVm>().ReverseMap();
        }
    }
}
