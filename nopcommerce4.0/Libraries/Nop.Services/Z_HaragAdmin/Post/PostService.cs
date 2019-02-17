using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Z_Harag;
using Nop.Core.Data;

namespace Nop.Services.Z_HaragAdmin.Post
{
    public class PostService : IPostService
    {
        #region Fields
        private readonly IRepository<Z_Harag_Post> _postRepository;
        #endregion
        #region Ctor
        public PostService(IRepository<Z_Harag_Post> postRepository)
        {
            this._postRepository = postRepository;
        }
        #endregion

        #region Methods
        public List<Z_Harag_Post> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
