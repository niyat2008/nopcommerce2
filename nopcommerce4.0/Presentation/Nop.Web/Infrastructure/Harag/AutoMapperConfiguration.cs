using AutoMapper;
using Nop.Core.Domain.Z_Harag;
using Nop.Core.Infrastructure.Mapper;
using Nop.Web.Models.Harag.Category;
using Nop.Web.Models.Harag.Comment;
using Nop.Web.Models.Harag.Post;
//using Nop.Web.Models.Harag.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Infrastructure.Harag
{
    public class AutoMapperConfiguration : Profile, IMapperProfile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Z_Harag_Category, CategoryModel>()
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore())
                .ForMember(dest => dest.Id,
                    mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    mo => mo.MapFrom(src => src.Name));


            //CreateMap<Z_Harag_Category, CategoryWithSubCategoriesModel>()
            //    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore())
            //    .ForMember(dest => dest.Id,
            //        mo => mo.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Name,
            //        mo => mo.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.SubCategories,
            //        mo => mo.MapFrom(src => src.SubCategories));


            //CreateMap<Z_Harag_SubCategory, SubCategoryModel>()
                //.ForMember(dest => dest.CustomProperties, mo => mo.Ignore())
                //.ForMember(dest => dest.Id,
                //    mo => mo.MapFrom(src => src.Id))
                //.ForMember(dest => dest.Name,
                //    mo => mo.MapFrom(src => src.Name));

            CreateMap<Z_Harag_Post, PostModel>()
                   .ForMember(dest => dest.CustomProperties, mo => mo.Ignore())
                   .ForMember(dest => dest.Id,
                    mo => mo.MapFrom(src => src.Id))
                   .ForMember(dest => dest.CategoryId,
                    mo => mo.MapFrom(src => src.CategoryId))
                    .ForMember(dest => dest.DateCreated,
                    mo => mo.MapFrom(src => src.DateCreated))
                    .ForMember(dest => dest.DateUpdated,
                    mo => mo.MapFrom(src => src.DateUpdated))
                    .ForMember(dest => dest.IsAnswered,
                    mo => mo.MapFrom(src => src.IsAnswered))
                    .ForMember(dest => dest.IsClosed,
                    mo => mo.MapFrom(src => src.IsClosed))
                    .ForMember(dest => dest.Rate,
                    mo => mo.MapFrom(src => src.Rate))
                    .ForMember(dest => dest.IsDispayed,
                    mo => mo.MapFrom(src => src.IsDispayed))
                    .ForMember(dest => dest.IsReserved,
                    mo => mo.MapFrom(src => src.IsReserved))
             
                    .ForMember(dest => dest.Text,
                    mo => mo.MapFrom(src => src.Text))
                    .ForMember(dest => dest.Title,
                    mo => mo.MapFrom(src => src.Title))
                    .ForMember(dest => dest.PostOwner,
                    mo => mo.MapFrom(src => src.Customer.Username));

            //.ForMember(dest => dest.SubCategoryId,
            //        mo => mo.MapFrom(src => src.SubCategoryId))


            CreateMap<Z_Harag_Post, PostWithFilesModel>()
                   .ForMember(dest => dest.CustomProperties, mo => mo.Ignore())
                   .ForMember(dest => dest.Id,
                    mo => mo.MapFrom(src => src.Id))
                   .ForMember(dest => dest.CategoryId,
                    mo => mo.MapFrom(src => src.CategoryId))
                    .ForMember(dest => dest.DateCreated,
                    mo => mo.MapFrom(src => src.DateCreated))
                    .ForMember(dest => dest.DateUpdated,
                    mo => mo.MapFrom(src => src.DateUpdated))
                    .ForMember(dest => dest.IsAnswered,
                    mo => mo.MapFrom(src => src.IsAnswered))
                    .ForMember(dest => dest.IsClosed,
                    mo => mo.MapFrom(src => src.IsClosed))
                    .ForMember(dest => dest.Rate,
                    mo => mo.MapFrom(src => src.Rate))
                    .ForMember(dest => dest.IsDispayed,
                    mo => mo.MapFrom(src => src.IsDispayed))
                    .ForMember(dest => dest.IsReserved,
                    mo => mo.MapFrom(src => src.IsReserved))
                   
                    .ForMember(dest => dest.Text,
                    mo => mo.MapFrom(src => src.Text))
                    .ForMember(dest => dest.Title,
                    mo => mo.MapFrom(src => src.Title))
                    .ForMember(dest => dest.PostOwner,
                    mo => mo.MapFrom(src => src.Customer.Username));


            //.ForMember(dest => dest.SubCategoryId,
            //        mo => mo.MapFrom(src => src.SubCategoryId))

            CreateMap<Z_Harag_Comment, CommentModel>()
                   .ForMember(dest => dest.CustomProperties, mo => mo.Ignore())
                   .ForMember(dest => dest.Id,
                    mo => mo.MapFrom(src => src.Id))
                   .ForMember(dest => dest.Text,
                    mo => mo.MapFrom(src => src.Text))
                    .ForMember(dest => dest.DateCreated,
                    mo => mo.MapFrom(src => src.DateCreated))
                    .ForMember(dest => dest.DateUpdated,
                    mo => mo.MapFrom(src => src.DateUpdated))
                    .ForMember(dest => dest.CommentedBy,
                    mo => mo.MapFrom(src => src.CommentedBy))
                    .ForMember(dest => dest.PostId,
                    mo => mo.MapFrom(src => src.PostId));

        }

        public int Order => 0;
    }
}
