using AutoMapper;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL
{
    public class MappingProfile:Profile
    {
         public MappingProfile()
        {
            CreateMap<Session,SessionViewModel>()
                .ForMember(dest => dest.CategoryName, options=>options.MapFrom(src => src.category.Name))
             .ForMember(dest => dest.TraineeName, options => options.MapFrom(src => src.trainer.name))
              .ForMember(dest => dest.AvailableSlots, options=>options.Ignore());



        }

    }
}
