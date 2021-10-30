//using AutoMapper;
//using Library.Models.Entites;
//using Library.Models.RequsetDTO;
//using Library.Models.ResponseDTO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Library.Models
//{
    
//        public class AutoMapperProfile : Profile
//        {
//            public AutoMapperProfile()
//            {
//                CreateMap<AuthorAddRequsetDTO, Authors>()
//                    .ForMember(dest => dest.IsDeleted,
//                               opt => opt.MapFrom(src => false));

//                CreateMap<AuthorUpdateRequsetDTO, Authors>();
//                CreateMap<Authors, AuthorResponseDTO>()
//                         .ForMember(dest => dest.BookCount,
//                              opt => opt.MapFrom(src => src.Books.Count()));
               
//            }
//        }
    
//}
