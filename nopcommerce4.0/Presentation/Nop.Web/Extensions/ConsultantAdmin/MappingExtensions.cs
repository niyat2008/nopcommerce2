﻿using Nop.Core.Domain.Z_Consultant;
using Nop.Core.Infrastructure.Mapper;
using Nop.Web.Models.Consultant.Category;
using Nop.Web.Models.ConsultantAdmin.Comment;
using Nop.Web.Models.ConsultantAdmin.Post;
using Nop.Web.Models.Consultant.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Web.Models.ConsultantAdmin.Post;

namespace Nop.Web.Extensions.ConsultantAdmin
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }




        #region Category

        public static CategoryModel ToCategoryModel(this Z_Consultant_Category entity)
        {
            return entity.MapTo<Z_Consultant_Category, CategoryModel>();
        }

        public static Z_Consultant_Category ToEntity(this CategoryModel model)
        {
            return model.MapTo<CategoryModel, Z_Consultant_Category>();
        }

        public static Z_Consultant_Category ToEntity(this CategoryModel model, Z_Consultant_Category destination)
        {
            return model.MapTo(destination);
        }





        public static CategoryWithSubCategoriesModel ToCategoryWithSubCategoriesModel(this Z_Consultant_Category entity)
        {
            return entity.MapTo<Z_Consultant_Category, Models.Consultant.Category.CategoryWithSubCategoriesModel>();
        }

        public static Z_Consultant_Category ToEntity(this CategoryWithSubCategoriesModel model)
        {
            return model.MapTo<CategoryWithSubCategoriesModel, Z_Consultant_Category>();
        }

        public static Z_Consultant_Category ToEntity(this CategoryWithSubCategoriesModel model, Z_Consultant_Category destination)
        {
            return model.MapTo(destination);
        }

        #endregion






        #region SubCategory

        public static SubCategoryModel ToSubCategoryModel(this Z_Consultant_SubCategory entity)
        {
            return entity.MapTo<Z_Consultant_SubCategory, SubCategoryModel>();
        }

        public static Z_Consultant_SubCategory ToEntity(this SubCategoryModel model)
        {
            return model.MapTo<SubCategoryModel, Z_Consultant_SubCategory>();
        }

        public static Z_Consultant_SubCategory ToEntity(this SubCategoryModel model, Z_Consultant_SubCategory destination)
        {
            return model.MapTo(destination);
        }

        #endregion


        #region Post

        public static Models.ConsultantAdmin.Post.PostModel ToPostModel(this Z_Consultant_Post entity)
        {
            return entity.MapTo<Z_Consultant_Post, Models.ConsultantAdmin.Post.PostModel>();
        }

        public static Z_Consultant_Post ToEntity(this Models.ConsultantAdmin.Post.PostModel model)
        {
            return model.MapTo<Models.ConsultantAdmin.Post.PostModel, Z_Consultant_Post>();
        }

        public static Z_Consultant_Post ToEntity(this Models.ConsultantAdmin.Post.PostModel model, Z_Consultant_Post destination)
        {
            return model.MapTo(destination);
        }




        public static PostWithFilesModel ToPostWithFilesModel(this Z_Consultant_Post entity)
        {
            return entity.MapTo<Z_Consultant_Post, PostWithFilesModel>();
        }

        public static Z_Consultant_Post ToEntity(this PostWithFilesModel model)
        {
            return model.MapTo<PostWithFilesModel, Z_Consultant_Post>();
        }

        public static Z_Consultant_Post ToEntity(this PostWithFilesModel model, Z_Consultant_Post destination)
        {
            return model.MapTo(destination);
        }

        #endregion


        #region Post
        public static CommentModel ToCommentModel(this Z_Consultant_Comment entity)
        {
            return entity.MapTo<Z_Consultant_Comment, CommentModel>();
        }

        public static Z_Consultant_Comment ToEntity(this CommentModel model)
        {
            return model.MapTo<CommentModel, Z_Consultant_Comment>();
        }

        public static Z_Consultant_Comment ToEntity(this CommentModel model, Z_Consultant_Comment destination)
        {
            return model.MapTo(destination);
        }
        #endregion
    }
}
