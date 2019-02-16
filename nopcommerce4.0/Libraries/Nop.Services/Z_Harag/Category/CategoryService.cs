using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Category
{
   public class CategoryService:ICategoryService
    {
        private readonly IRepository<Z_Harag_Category> _categoryRepository;
        private readonly IEventPublisher _eventPublisher;

        public CategoryService(IRepository<Z_Harag_Category> categoryRepository, IEventPublisher eventPublisher)
        {
            this._categoryRepository = categoryRepository;
            this._eventPublisher = eventPublisher;
        }
    }
}
