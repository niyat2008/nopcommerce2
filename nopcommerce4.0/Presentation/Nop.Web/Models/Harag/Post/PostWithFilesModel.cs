﻿using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Post
{
    public class PostWithFilesModel : BaseNopEntityModel
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsClosed { get; set; }
        public int? Rate { get; set; }
        public bool IsAnswered { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsDispayed { get; set; }
        public double Len { get; set; }
        public double Lat { get; set; }
        public bool IsReserved { get; set; }
        public int CategoryId { get; set; } 
        public string CategoryName { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; } 
        public int userId { get; set; } 
        public string PostOwner { get; set; }
        public string Contact { get; set; }

        public IList<string> Files { get; set; }

        public PostWithFilesModel()
        {
            Files = new List<string>();
        }
    }
}
