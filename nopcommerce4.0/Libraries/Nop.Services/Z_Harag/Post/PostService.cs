using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Z_Harag.Helpers;

namespace Nop.Services.Z_Harag.Post
{

    public class PostService : IPostService
    {

        private readonly IRepository<Z_Harag_Post> _postRepository;
        private readonly IRepository<Z_Harag_Category> _categoryRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;
        ILocalizationService _localizationService;


        public PostService(IRepository<Z_Harag_Post> postRepository,
            IRepository<Z_Harag_Category> categoryRepository,
            ILocalizationService localizationService,
            IHostingEnvironment env,
        IEventPublisher eventPublisher)
        {
            this._postRepository = postRepository;
            this._categoryRepository = categoryRepository;
            this._eventPublisher = eventPublisher;
            this._env = env;
            this._localizationService = localizationService;
        }


    }
}