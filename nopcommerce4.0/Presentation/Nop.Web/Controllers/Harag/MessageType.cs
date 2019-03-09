using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Web.Extensions.Harag;

using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_Harag.Helpers;
using Nop.Services.Z_Harag.Message;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Models.Harag.Message;

namespace Nop.Web.Controllers.Harag
{
    public enum MessageType
    {
        User = 1,
        Post = 2,
        Comment = 3
    }
}