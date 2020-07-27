using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly.AutoMapperProfile
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Customer mapping
            CreateMap<Customer, CustomerDto>();

            CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            // Movie mapping
            CreateMap<Movie, MovieDto>();

            CreateMap<MovieDto, Movie>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            // MembershitType mapping
            CreateMap<MembershipType, MemberTypeDto>();

            // Genre mapping
            CreateMap<Genre, GenreDto>();

        }  
    }
}
