﻿using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Category
{
   public interface ICategoryService
    {
        List<Z_Harag_Category> GetCategories();
    }
}