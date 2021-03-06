﻿using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Post
{
   public interface IPostService
    {
        //Get All Posts
        List<Z_Harag_Post> GetAllPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection);
        //Get All Posts
         List<Z_Harag_Post> GetAllDeletedPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection);
        //Delete Post
        void DeletePost(int id);
        //post Details
        Z_Harag_Post PostDetails(int postId);
        //post Message
        List<Z_Harag_Message> GetPostMessage(int postId);
        //Post Reports
        List<Z_Harag_Reports> GetPostReports(int postId);
        //Get Post ByID
        List<Z_Harag_Post> GetPostById(int postId);


        //Get Posts By CategoryId
        List<Z_Harag_Post> GetPostsByCategory(int categoryId, int start, int length, string searchValue, string sortColumnName, string sortDirection);

        //Get Posts By CityId
        List<Z_Harag_Post> GetPostsByCity(int cityId, int start, int length, string searchValue, string sortColumnName, string sortDirection);

        //Get Posts By Date
        //List<Z_Harag_Post> GetPostsByDate(DateTime date1);
    }
}
