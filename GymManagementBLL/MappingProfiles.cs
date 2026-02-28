using AutoMapper;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entites;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.Data.SqlClient;
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
            MapSession();
            MapMembers();
          



        }
        private void MapSession()
        {
            CreateMap<Session, SessionViewModel>()
               .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.category.Name))
            .ForMember(dest => dest.TraineeName, options => options.MapFrom(src => src.trainer.name))
             .ForMember(dest => dest.AvailableSlots, options => options.Ignore());

            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();
        }

        private void MapMembers()
        {
            //way 1
            //CreateMap<CreateMemberViewModel, Member>()
            //    .ForMember(s => s.address, options => options.MapFrom(src => new Address()
            //    {
            //        BuildingNumber = src.BuildingNumber,
            //        street = src.Street,
            //        City = src.City,
            //    }));

            //way 2
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(m => m.address, options => options.MapFrom(src => src))  // will take the mapping from the map of creatememberviewmodel to address
                .ForMember(m=>m.HealthRecord,options=>options.MapFrom(src=>src.HealthRecordViewModel)); //will take the map from the mapping of healthrecordviewmodel to healthrecord

            CreateMap<CreateMemberViewModel, Address>()
                .ForMember(a => a.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
                 .ForMember(a => a.street, opt => opt.MapFrom(src => src.Street))
                 .ForMember(a => a.City, opt => opt.MapFrom(src => src.City));

            CreateMap<HealthRecord, HealthRecordViewModel>();

            CreateMap<Member, MemberViewModel>()
                .ForMember(dest=>dest.Gender,opt=>opt.MapFrom(src=>src.gender.ToString()))
                .ForMember(dest=>dest.DateOfBirth,opt=>opt.MapFrom(src=>src.DateOfBirth.ToShortDateString()))
                .ForMember(dest=>dest.Address,opt=>opt.MapFrom(src=> $"{src.address.BuildingNumber}-{src.address.street}-{src.address.City}"));

            CreateMap<Member, MemberUpdateViewModel>()
              .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.address.BuildingNumber))
              .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.address.street))
               .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.address.City));

            CreateMap<MemberUpdateViewModel, Member>()
                .ForMember(dest => dest.name, opt => opt.Ignore())
                .ForMember(dest => dest.photo, opt => opt.Ignore())   // they are for displaying only so not map this if the user chnage their values by anyway
                //.ForMember(dest=>dest.address.BuildingNumber,opt=>opt.MapFrom(src=>src.BuildingNumber))     error as the automapper allows the dest must be top level property                
                .AfterMap((src, dest)   //after map make a code c# after mapping and here for updating the values in db
                =>
                {
                    dest.address.BuildingNumber = src.BuildingNumber;
                dest.address.street = src.Street;
                    dest.address.City = src.City;
                    dest.Updated_At=DateTime.Now;
                    }

                ); 


        }

    }
}
