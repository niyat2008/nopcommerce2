using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Z_Harag.Helpers;

using static Nop.Services.Z_Consultant.Post.PostService;

namespace Nop.Services.Z_Harag.Post
{

    public class PostService : IPostService
    {
        #region Fields
        private readonly IRepository<Z_Harag_Post> _postRepository; 
        private readonly IRepository<Z_Harag_BlackList> _blacklistRepository;
        private readonly IRepository<Z_Harag_Category> _categoryRepository;
        private readonly IRepository<Z_Harag_Photo> _photoRepository;
        private readonly IRepository<Z_Harag_Favorite> _favRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Neighborhood> _neighborhoodRepository;
        private readonly IRepository<Z_Harag_Comment> _commentRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;
        private readonly IRepository<Z_Harag_Reports> _reportRepository;

        ILocalizationService _localizationService;
        #endregion


        #region Ctor

        public PostService(IRepository<Z_Harag_Post> postRepository,
            IRepository<Z_Harag_Category> categoryRepository, IRepository<Z_Harag_Comment> _commentRepository,
             IRepository<Z_Harag_Photo> _photoRepository,
             IRepository<Z_Harag_BlackList> _blacklistRepository,
             IRepository<City> _cityRepository,
             IRepository<Z_Harag_Reports> _reportRepository,
             IRepository<Neighborhood> _neighborhoodRepository,
             IRepository<Z_Harag_Favorite> _favRepository,
 IRepository<Customer> _customerRepository,
            ILocalizationService localizationService,
            IHostingEnvironment env,
        IEventPublisher eventPublisher)
        {
            this._blacklistRepository = _blacklistRepository;
            this._customerRepository = _customerRepository;
            this._postRepository = postRepository;
            this._favRepository = _favRepository;
            this._neighborhoodRepository = _neighborhoodRepository;
            this._cityRepository = _cityRepository;
            this._categoryRepository = categoryRepository;
            this._eventPublisher = eventPublisher;
            this._commentRepository = _commentRepository;
            this._photoRepository = _photoRepository; 
            this._reportRepository = _reportRepository;
            this._env = env;
            this._localizationService = localizationService;
        }


        #endregion 

        #region Methods
        private bool DeletePostPhohos(int postId)
        {
            var photos = _photoRepository.Table.Where(m => m.PostId == postId).ToList();

            foreach (var item in photos)
            {
                _photoRepository.Delete(item);
            }
            return true;
        }
        public Z_Harag_Post UpdatePost(PostForPostListModel postForPostModel, int cutomerId, IList<string> files, List<string> errors)
        {
            DeletePostPhohos(postForPostModel.Id);

            List<KeyAndValue> filesUrl = UploadFiles(files, errors);

            if (errors.Count > 0)
            {
                return null;
            }
            var postE = _postRepository.Table.Where(m => m.Id == postForPostModel.Id).FirstOrDefault();
            
            postE.Text = postForPostModel.Text;
            postE.Title = postForPostModel.Title;
            postE.CategoryId = postForPostModel.CategoryId;
                postE.CityId = postForPostModel.CityId; 
                postE.Contact = postForPostModel.Contact;
                postE.DateUpdated = DateTime.Now;
             

            foreach (var item in filesUrl)
            {
                if (item.Key.ToLower() == "image")
                    postE.Z_Harag_Photo.Add(new Z_Harag_Photo()
                    {
                        Url = item.Value
                    });
            } 
            _postRepository.Update(postE);


            return postE;
        }

        public Z_Harag_Post AddNewPost(PostForPostListModel postForPostModel, int cutomerId, IList<string> files, List<string> errors)
        {
            List<KeyAndValue> filesUrl = UploadFiles(files, errors);

            if (errors.Count > 0)
            {
                return null;
            }

            var post = new Z_Harag_Post()
            {
                Text = postForPostModel.Text,
                Title = postForPostModel.Title,
                CategoryId = postForPostModel.CategoryId,
                CityId = postForPostModel.CityId,
                Contact = postForPostModel.Contact,
                CustomerId = cutomerId,
                DateCreated = DateTime.Now,
                IsAnswered = false,
                IsCommentingClosed = false,
                IsDispayed = false,
                DateUpdated = DateTime.Now ,
                IsDeleted = false,
                IsOrder = postForPostModel.IsOrder,
                IsFeatured = false,
                IsCommon = false,
                Tags = postForPostModel.Tags
            };
            foreach (var item in filesUrl)
            {
                if (item.Key.ToLower() == "image")
                    post.Z_Harag_Photo.Add(new Z_Harag_Photo()
                    {
                        Url = item.Value
                    }); 
            }
             
            _postRepository.Insert(post);
             

            return post;
        }

        
        public bool IsDisplayed(int postId)
        {
            var query = _postRepository.TableNoTracking;
            var post = query.Where(m => m.Id == postId).FirstOrDefault();

            if (post == null)
                return false;


            return post.IsDispayed;
        }

        public bool IsExists(int postId)
        {
            var query = _postRepository.TableNoTracking;
            var post = query.Where(m => m.Id == postId).FirstOrDefault();

            if (post == null)
                return false;


            return true;
        }

        
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public List<KeyAndValue> UploadFiles(IList<string> files, List<string> errors)
        {


            int numberOfPhotosAllowed = 5;
            if (!int.TryParse(_localizationService.GetResource("Consultant.Api.Post.numberOfPhotosAllowed"), out numberOfPhotosAllowed))
            {
                numberOfPhotosAllowed = 5;
            }


            int SizeOfPhotoAllowedInMb = 50;
            if (!int.TryParse(_localizationService.GetResource("Consultant.Api.Post.SizeOfPhotoAllowedInMb"), out SizeOfPhotoAllowedInMb))
            {
                SizeOfPhotoAllowedInMb = 50;
            }


            List<KeyAndValue> filesUrl = new List<KeyAndValue>();

            var ImagesPath = Path.Combine(_env.WebRootPath, "HaragApi\\Uploads\\Images");
            var logoImage = Path.Combine(_env.WebRootPath, "images\\thumbs\\watermark.png");
            //var Vedios = Path.Combine(_env.WebRootPath, "Uploads\\Videos");

            int MaxContentLength = 1024 * 1024 * SizeOfPhotoAllowedInMb; //Size = 1*SizeOfPhotoAllowedInMb  MB   


            if (files.Count > numberOfPhotosAllowed)
                errors.Add("No more than " + numberOfPhotosAllowed + " images allowed");

            if (errors.Count > 0)
            {
                return filesUrl;
            }
            else
            {
                List<ImageBytesAndFormat> listOfImagesInBytes = new List<ImageBytesAndFormat>();

                foreach (var fileString in files)
                {
                    byte[] imageBytes = Convert.FromBase64String(fileString);

                    ImageFormat imageformate = null;
                    string imageFormateName = string.Empty;
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        var imagesize = ms.Length;
                        if (imagesize > MaxContentLength)
                        {
                            errors.Add("Image size must be less than" + SizeOfPhotoAllowedInMb + " MB");
                            return filesUrl;
                        }
                        else
                        {
                            Image image = Image.FromStream(ms);
                            
                            imageformate = image.RawFormat;

                            bool acceptFormat = false;

                            if (ImageFormat.Bmp.Equals(imageformate))
                            {
                                acceptFormat = true;
                                imageFormateName = "Bmp";
                            }
                            else if (ImageFormat.Emf.Equals(imageformate))
                            {
                                acceptFormat = true;
                                imageFormateName = "Emf";
                            }
                            else if (ImageFormat.Exif.Equals(imageformate))
                            {
                                acceptFormat = true;
                                imageFormateName = "Exif";
                            }
                            else if (ImageFormat.Gif.Equals(imageformate))
                            {
                                acceptFormat = true;
                                imageFormateName = "Gif";
                            }
                            else if (ImageFormat.Icon.Equals(imageformate))
                            {
                                acceptFormat = true;
                                imageFormateName = "Icon";
                            }
                            else if (ImageFormat.Jpeg.Equals(imageformate))
                            {
                                acceptFormat = true;
                                imageFormateName = "Jpeg";
                            }
                            else if (ImageFormat.Png.Equals(imageformate))
                            {
                                acceptFormat = true;
                                imageFormateName = "Png";
                            }
                            else if (ImageFormat.Tiff.Equals(imageformate))
                            {
                                acceptFormat = true;
                                imageFormateName = "Tiff";
                            }
                            else if (ImageFormat.Wmf.Equals(imageformate))
                            {
                                acceptFormat = true;
                                imageFormateName = "Wmf";
                            }

                            if (!acceptFormat)
                            {
                                errors.Add("Image formt not allowed");
                                return filesUrl;
                            }

                        }

                        listOfImagesInBytes.Add(new ImageBytesAndFormat() { ImageBytes = imageBytes, ImageFormateName = imageFormateName });
                    }
                }

                foreach (var file in listOfImagesInBytes)
                {
                    int randomNumber = new Random().Next();
                    var fileName = randomNumber + Path.GetRandomFileName() + "." + file.ImageFormateName;
                    var filePath = Path.Combine(ImagesPath, fileName);

                    using (var ms = new MemoryStream(file.ImageBytes))
                    {
                        var image = DrawLogo(logoImage, Image.FromStream(ms));
                        image.Save(filePath);
                    }
                        // File.WriteAllBytes(filePath, file.ImageBytes);
                    filesUrl.Add(new KeyAndValue() { Key = "image", Value = fileName });
                }
            }

            return filesUrl;
        }

        private Bitmap DrawLogo(string logo, Image img )
        {
            var image = new Bitmap(img);

            Image ib = Image.FromFile(logo); // This is 300x300 
            ib = ResizeImage(ib, img.Width / 6, img.Height / 6);
            using (Graphics g = Graphics.FromImage(image))
            {
                //g.DrawImage(ib, 0, 0, img.Width / 6, img.Height / 6);
                g.DrawImage(ib, image.Width - (img.Width / 6), image.Height - (img.Height / 6), (img.Width / 6), (img.Height / 6));
            }

            return image;

        }

        //Get  Post By Id
        public List<Z_Harag_Post> GetPostById(int postId, string type = "")
        {
            if (type == "ClosedDisplayed")
            {
                //var post = _postRepository.TableNoTracking.Where(p => p.Id == postId && p.IsClosed==true && p.IsDispayed==true);
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId && p.IsCommentingClosed == true && p.IsDispayed == true).ToList();
            }
            if (type == "ClosedNotDisplayed")
            {
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId && p.IsCommentingClosed == true && p.IsDispayed == false).ToList();
            }
            if (type == "NotReplied")
            {
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId && p.IsAnswered == false).ToList();
            }
            if (type == "Reserved")
            {
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId && p.IsFeatured == true).ToList();
            }
            if (type == "Open")
            {
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId && p.IsCommentingClosed == false && p.IsDispayed == false && p.IsAnswered == true).ToList();
            }
            else
            {
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId  && p.IsDispayed == true  ).ToList();

            }  
        }
        //Remove Closed Displayed Post
        public void RemovePost(int postId)
        {

            var post = _postRepository.Table.Where(p => p.Id == postId).FirstOrDefault();
            if (post != null)
            {
                var comments = _commentRepository.Table.Where(c => c.PostId == postId).ToList();
                if (comments != null)
                    _commentRepository.Delete(comments);

                var photos = _photoRepository.Table.Where(c => c.PostId == postId).ToList();
                if (photos != null)
                {

                    _photoRepository.Delete(photos);
                    foreach (var photo in photos)
                    {

                    }
                }
                _postRepository.Delete(post);
            }

        }

        public Z_Harag_Post GetPost(int postId, string type)
        {
            var post = _postRepository.Table.Include(p => p.Z_Harag_Photo)
                    .Include(p => p.Customer)
                    .Include(p => p.Category)
                    .Include(p => p.City).Where(p => p.Id == postId).FirstOrDefault();
            if (post != null)
            { 
                return post;
            }
            return null;
        }

        public List<Z_Harag_Post> GetFeaturedPosts(PagingParams pagingParams)
        { 
             var featured = _postRepository.TableNoTracking
                .Include(m => m.Category)
                .Include(m => m.Customer)
                .Include(m => m.City)
                .Include(m => m.Z_Harag_Photo)
                .Include(m => m.Z_Harag_Comment)
                .Where(p =>  p.IsDeleted == false && p.IsFeatured == false)
                .OrderByDescending(r => r.DateUpdated)
                .Skip(pagingParams.PageNumber * pagingParams.PageSize)
                .Take(pagingParams.PageSize).ToList();

            var result = _postRepository.TableNoTracking
                .Include(m => m.Category)
                .Include(m => m.Customer)
                .Include(m => m.City)
                .Include(m => m.Z_Harag_Photo)
                .Include(m => m.Z_Harag_Comment)
                .Where(m => m.IsFeatured == true).ToList();

            result.AddRange(featured);

            return result;
        }
        public List<Z_Harag_Post> GetTagsPost(PagingParams pagingParams, string tag)
        {
            
            var result = _postRepository.TableNoTracking
                .Include(m => m.Category)
                .Include(m => m.Customer)
                .Include(m => m.City)
                .Include(m => m.Z_Harag_Photo)
                .Include(m => m.Z_Harag_Comment)
                .Where(m => m.IsDeleted == false && m.Tags.Contains(tag)).OrderByDescending(mbox => mbox.DateUpdated).Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            return result;
        }
        public List<City> GetCities()
        {
            var query = _cityRepository.TableNoTracking.ToList();
            return query;
        }

        public bool CanAddNewPost(int userId)
        {
            var query = _postRepository.TableNoTracking;
            var result = query.Where(m => m.CustomerId == userId).OrderByDescending(m => m.DateUpdated).FirstOrDefault();
            if(result == null)
            {
                return true;
            }
            else if( (DateTime.Now - result.DateUpdated) < new TimeSpan(24, 0, 0 ))
            {
                return false;
            }
            return true;
        }



        public bool EditHaragPost(int postId, Z_Harag_Post post)
        {
            var isExists =  this.IsExists(postId);

            if (!isExists)
                return false;

            _postRepository.Update(post);

            return true;
        }

        public List<Z_Harag_Post> SearchByCategory(int catId, PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking
                .Include(m => m.Category)
                .Include(m => m.Customer)
                .Include(m => m.City)
                .Include(m => m.Z_Harag_Photo)
                .Include(m => m.Z_Harag_Comment)
                .Where(c => c.CategoryId == catId&&c.IsDeleted == false).OrderByDescending(mbox => mbox.DateUpdated).Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            return query;
        }

        public List<Z_Harag_Post> SearchByCity(int catId, PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking
                .Include(m => m.Category)
                .Include(m => m.Customer)
                .Include(m => m.City)
                .Include(m => m.Z_Harag_Photo)
                .Include(m => m.Z_Harag_Comment)
                .Where(c => c.CityId == catId&& c.IsDeleted == false).OrderByDescending(mbox => mbox.DateUpdated).Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            return query;
        }

        public List<Z_Harag_Post> SearchByDate(DateTime date, PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking
                .Include(m => m.Category)
                .Include(m => m.Customer)
                .Include(m => m.City)
                .Include(m => m.Z_Harag_Photo)
                .Include(m => m.Z_Harag_Comment)
                 .Where(c => c.DateCreated.Day == date.Day&& c.IsDeleted == false)
                 .OrderByDescending(mbox => mbox.DateUpdated ).Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            return query;
        }

        public List<Z_Harag_Post> SearchByNeighborhood(int id, PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking
                .Include(m => m.Category)
                .Include(m => m.Customer)
                .Include(m => m.City)
                .Include(m => m.Z_Harag_Photo)
                .Include(m => m.Z_Harag_Comment)
                .Where(c => c.NeighborhoodId == id&& c.IsDeleted == false).OrderByDescending(mbox => mbox.DateUpdated).Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            return query;
        }

        public List<Z_Harag_Post> GetUserPosts(int userId, PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking
                .Include(m => m.Category)
                .Include(m => m.Customer)
                .Include(m => m.City)
                .Include(m => m.Z_Harag_Photo)
                .Include(m => m.Z_Harag_Comment)
                 .Where(c => c.CustomerId == userId&& c.IsDeleted == false).OrderByDescending(mbox => mbox.DateUpdated).Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            return query;
        }

        public List<Z_Harag_Post> GetCurrentUserPosts(int userId, PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking
                .Include(m=> m.Category)
                .Include(m => m.Customer)
                .Include(m => m.City)
                .Include(m => m.Z_Harag_Photo)
                .Include(m => m.Z_Harag_Comment)
                .Where(c => c.CustomerId == userId&& c.IsDeleted == false)
                  .OrderByDescending(mbox => mbox.DateUpdated).Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            return query;
        }

        public int UpdatePostLocation(UpdatePostLocationModel post)
        {
            var isExists = this.IsExists(post.PostId);

            if (!isExists)
                return 0;

            var postE = _postRepository.Table.Where(m => m.Id == post.PostId).FirstOrDefault();

            postE.CityId = post.CityId;

            if (post.NeighborhoodId == 0)
            {
                postE.NeighborhoodId = null;
            }
            else
            {
                postE.NeighborhoodId = post.NeighborhoodId;
            }

         

            _postRepository.Update(postE);

            return postE.Id;
            
        }

        public bool AddPostToFavorite(int postId, int userId)
        {
            var query = _postRepository.TableNoTracking.Where(p => p.Id == postId).FirstOrDefault();

            if (query == null)
            {
                return false;
            }

            if (this.IsPostAddedToFavorite(postId, userId))
            {
                return false;
            }

            _favRepository.Insert(new Z_Harag_Favorite
            {
                CustomerId = userId,
                PostId = postId
            });

            return true;
        }

        
        public bool AddPostToBlackList(int postId, int userId)
        {
            var isExists = this.IsExists(postId);

            if (!isExists)
            {
                return false;
            }

            var post = GetPost(postId, "");

            return true;
        }

        public bool FollowReplysOnPost(int postId, int userId)
        {
            throw new NotImplementedException();
        }

        public List<Z_Harag_Post> SearchByDate(int catId)
        {
            throw new NotImplementedException();
        }

        

        public bool IsPostAddedToFavorite(int postId, int userId)
        {
            var query = _favRepository.TableNoTracking.Where(p => p.PostId == postId && p.CustomerId == userId).FirstOrDefault();

            if (query == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Neighborhood> GetNeighborhoods(int cityId)
        {
            var query = _neighborhoodRepository.TableNoTracking.Where(n => n.CityId == cityId).ToList();
            return query;
        }

        public Z_Harag_Reports ReportPost( Z_Harag_Reports report )
        {
            _reportRepository.Insert(report);
            return report;
        }

        public bool RemovePostFromFavorite(int postId, int userId)
        {

            var query = _postRepository.Table.Where(p => p.Id == postId).FirstOrDefault();

            if (query == null)
            {
                return false;
            }
            var fav = _favRepository.Table.Where(m => m.CustomerId == userId && m.PostId == postId).FirstOrDefault();

            if (fav != null)
            {
                _favRepository.Delete(fav);
                return true;
            } 
            return false;
        }

        public City GetCity(string city)
        {
            var cityEn = _cityRepository.TableNoTracking.Where(m => m.ArName == city).FirstOrDefault();

            return cityEn;
        }
          
        List<Z_Harag_Post> IPostService.GetFavoritesPosts(int id, PagingParams pagingParams)
        {
            var posts = _favRepository.Table.Include(m => m.Customer).Include(m => m.Z_Harag_Post).Where(m => m.CustomerId == id)
                  .OrderByDescending(m => m.Id).Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            List<Z_Harag_Post> postsFav = new List<Z_Harag_Post>();

            foreach (var item in posts)
            {
                try
                {
                   postsFav.Add(this.GetPost(item.PostId, ""));
                }
                catch (Exception eee)
                { 
                } 
            }
            
            return postsFav;

        }
  
        public List<Z_Harag_Post> SearchByCategoryPage(int catId, PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking
              .Include(m => m.Category)
              .Include(m => m.Customer)
              .Include(m => m.City)
              .Include(m => m.Z_Harag_Photo)
              .Include(m => m.Z_Harag_Comment)
              .Where(c => c.CategoryId == catId  && c.IsDeleted == false)
                         .OrderByDescending(mbox => mbox.DateUpdated).Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            return query;
        }

        public List<Z_Harag_Post> SearchByCityPage(int catId,  PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking
              .Include(m => m.Category)
              .Include(m => m.Customer)
              .Include(m => m.City)
              .Include(m => m.Z_Harag_Photo)
              .Include(m => m.Z_Harag_Comment)
              .Where(c => c.CityId == catId && c.IsDeleted == false)
           .OrderByDescending(mbox => mbox.DateUpdated).Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            return query;
        }
        public List<Z_Harag_Post> SearchPosts(SearchModel searchModel, PagingParams pagingParams)
        {
            if (searchModel.City != 0)
            {
                pagingParams.PageNumber = 0;
                return _postRepository.TableNoTracking
              .Include(m => m.Category)
              .Include(m => m.Customer)
              .Include(m => m.City)
              .Include(m => m.Z_Harag_Photo)
              .Include(m => m.Z_Harag_Comment)
              .Where(c => (c.Text.Contains(searchModel.Term)
                  || c.Title.Contains(searchModel.Term)
                  || c.City.ArName.Contains(searchModel.Term)
                  || c.Category.Name.Contains(searchModel.Term)) && (c.CityId == searchModel.City)
                  && c.IsDeleted == false)
              .OrderByDescending(mbox => mbox.DateUpdated)
              .ToList()
              .Where(m => DateTime.Now.Subtract(m.DateUpdated).Days < searchModel.Time)
                .Skip(pagingParams.PageNumber * pagingParams.PageSize)
                .Take(pagingParams.PageSize).ToList();
            }
            else
            {
                return _postRepository.TableNoTracking
              .Include(m => m.Category)
              .Include(m => m.Customer)
              .Include(m => m.City)
              .Include(m => m.Z_Harag_Photo)
              .Include(m => m.Z_Harag_Comment).Where(c => (c.Text.Contains(searchModel.Term)
                 || c.Title.Contains(searchModel.Term)
                 || c.City.ArName.Contains(searchModel.Term)
                 || c.Category.Name.Contains(searchModel.Term)) && c.IsDeleted == false)
                 .OrderByDescending(mbox => mbox.DateUpdated)
                .Skip(pagingParams.PageNumber * pagingParams.PageSize)
                .Take(pagingParams.PageSize).ToList();
            } 
        }

        public List<Z_Harag_Post> SearchPostsCatCity(int cat, int city, PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking
            .Include(m => m.Category)
            .Include(m => m.Customer)
            .Include(m => m.City)
            .Include(m => m.Z_Harag_Photo)
            .Include(m => m.Z_Harag_Comment)
            .Where(c => c.CityId == city
            && c.CategoryId == cat && c.IsDeleted == false)
            .OrderByDescending(mbox => mbox.DateUpdated)
            .Skip(pagingParams.PageNumber * pagingParams.PageSize)
            .Take(pagingParams.PageSize).ToList();

            return query;
        }

        public List<Z_Harag_Post> GetLatestPosts(PagingParams pagingParams)
        {
            return _postRepository.TableNoTracking
               .Include(m => m.Category)
               .Include(m => m.Customer)
               .Include(m => m.City)
               .Include(m => m.Z_Harag_Photo)
               .Include(m => m.Z_Harag_Comment).Where(m => m.IsDeleted == false).OrderByDescending(r => r.DateCreated)
               .OrderByDescending(mbox => mbox.DateUpdated).OrderByDescending(mbox => mbox.IsFeatured).Skip(pagingParams.PageNumber* pagingParams.PageSize)
               .Take(pagingParams.PageSize).ToList();
        }
         
        public bool DeletePost(DeletePost deleteData)
        {
            var post = _postRepository.Table
                .Where(m => m.Id == deleteData.PostId)
                .FirstOrDefault();
             
            post.IsDeleted = true;
            post.DeleteMessage = deleteData.ReportMessage;
            post.DeleteTime = DateTime.Now;

            _postRepository.Update(post);

            return true;
        }

        public bool RefreshPost(int postId)
        {
            var post = _postRepository.Table.Where(m => m.Id == postId) .FirstOrDefault();

            if (post == null)
            {
                return false;
            }

            post.DateUpdated = DateTime.Now;

            _postRepository.Update(post);

            return true;
        }

        public bool hasFeaturedPost(int userId)
        {
            var posts = _postRepository
                .TableNoTracking.Where(m => m.IsFeatured == true).ToList();

            if (posts.Count > 0)
            {
                return true;
            }

            return false;
        }


        public bool IsFeaturedPost(int postId)
        {
            var posts = _postRepository
                .TableNoTracking.Where(m => m.Id == postId).FirstOrDefault();

            if (posts != null && posts.IsFeatured != null && posts.IsFeatured == true)
            {
                return true;
            }

            return false;
        }

        public bool SetFeatured(int postId)
        {
            var posts = _postRepository
               .Table.Where(m => m.Id == postId).FirstOrDefault();

            posts.IsFeatured = true;
            posts.LastFeaturedTime = DateTime.Now;

            _postRepository.Update(posts);
            return true;
        }

        public bool CloseCommenting(int postId)
        {
            var posts = _postRepository
               .Table.Where(m => m.Id == postId).FirstOrDefault();

            posts.IsCommentingClosed = true;

            _postRepository.Update(posts);
            return true;
        }

        public bool openCommenting(int postId)
        {
            var posts = _postRepository
               .Table.Where(m => m.Id == postId).FirstOrDefault();

            posts.IsCommentingClosed = false;

            _postRepository.Update(posts);
            return true;
        }

        public bool CanSetFeaturedPost(int userId)
        { 
            var date = _postRepository.TableNoTracking.Where(m => m.CustomerId == userId).Max(m => m.LastFeaturedTime);
            if (date == null)
            {
                return true;
            }
            if (DateTime.Now - date.Value > TimeSpan.FromDays(5) )
            {
                return true;
            }

            return false;
        }

        public List<Z_Harag_Post> GetHaragOrders(PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking
           .Include(m => m.Category)
           .Include(m => m.Customer)
           .Include(m => m.City)
           .Include(m => m.Z_Harag_Photo)
           .Include(m => m.Z_Harag_Comment)
           .Where(c => c.IsOrder == true && c.IsDeleted == false)
           .OrderByDescending(mbox => mbox.DateUpdated)
           .Skip(pagingParams.PageNumber * pagingParams.PageSize)
           .Take(pagingParams.PageSize).ToList();

            return query;
        }

        public List<string> GetTagsList(PagingParams pagingParams )
        {
            var query = _postRepository.TableNoTracking
             .GroupBy(m => m.Tags)
               .OrderByDescending(m => m.Count())  
              .Take(5).Select(m => m.Key).ToList();
            return query;
        }
        #endregion
    }
}