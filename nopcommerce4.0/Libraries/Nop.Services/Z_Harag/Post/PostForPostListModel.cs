using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Post
{
    public class PostForPostListModel
    {
        public PostForPostListModel()
        {
            Files = new List<string>();
        }

        public int Id { get; set; }
        public string Text { get; set; } 
        public string Title { get; set; } 
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        public int NeighborhoodId { get; set; }
        public string Contact { get; set; }
        public IList<string> Files { get; set; } 
        public double Len { get; set; }
        public double Lat { get; set; }


    }
}
