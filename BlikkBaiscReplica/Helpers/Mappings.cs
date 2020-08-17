using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlikkBaiscReplica.Models;
using BlikkBaiscReplica.Models.ViewModels;

namespace BlikkBaiscReplica.Helpers
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<ApplicationUser, ApplicationUserVm>().ReverseMap();
        }
    }
}
