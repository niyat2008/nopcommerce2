using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Categories
{
   public class PostCategoryModel
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated{ get; set; }
        public bool IsActive { get; set; }
        public string Title
        {
            get
            {
                return Id != 0 ? "   تعديل قسم" : "اضافة قسم";
            }
        }
        public PostCategoryModel()
        {
            Id = 0;
        }

    }
}
