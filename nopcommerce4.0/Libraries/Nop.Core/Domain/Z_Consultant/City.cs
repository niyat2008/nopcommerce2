﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Consultant
{
    public class City : BaseEntity
    {
        public int Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
    }
}