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
using static Nop.Services.Z_Consultant.Post.PostService;

namespace Nop.Services.Z_Harag.Post
{

    public class PostService : IPostService
    {
        #region Fields
        private readonly IRepository<Z_Harag_Post> _postRepository;
        private readonly IRepository<Z_Harag_Category> _categoryRepository;
        private readonly IRepository<Z_Harag_Photo> _photoRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Z_Harag_Comment> _commentRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;
        ILocalizationService _localizationService;
        #endregion


        #region Ctor

        public PostService(IRepository<Z_Harag_Post> postRepository,
            IRepository<Z_Harag_Category> categoryRepository, IRepository<Z_Harag_Comment> _commentRepository,
             IRepository<Z_Harag_Photo> _photoRepository,
             IRepository<City> _cityRepository,
            ILocalizationService localizationService,
            IHostingEnvironment env,
        IEventPublisher eventPublisher)
        {
            this._postRepository = postRepository;
            this._cityRepository = _cityRepository;
            this._categoryRepository = categoryRepository;
            this._eventPublisher = eventPublisher;
            this._commentRepository = _commentRepository;
            this._photoRepository = _photoRepository; 
            this._env = env;
            this._localizationService = localizationService;
        }


        #endregion

        #region Methods


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
                IsClosed = false,
                IsDispayed = false,
                IsReserved = false,
                IsCommon = false
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

                    File.WriteAllBytes(filePath, file.ImageBytes);
                    filesUrl.Add(new KeyAndValue() { Key = "image", Value = fileName });
                }
            }

            return filesUrl;
        }

        //Get  Post By Id
        public List<Z_Harag_Post> GetPostById(int postId, string type = "")
        {
            if (type == "ClosedDisplayed")
            {
                //var post = _postRepository.TableNoTracking.Where(p => p.Id == postId && p.IsClosed==true && p.IsDispayed==true);
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId && p.IsClosed == true && p.IsDispayed == true).ToList();
            }
            if (type == "ClosedNotDisplayed")
            {
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId && p.IsClosed == true && p.IsDispayed == false).ToList();
            }
            if (type == "NotReplied")
            {
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId && p.IsAnswered == false).ToList();
            }
            if (type == "Reserved")
            {
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId && p.IsReserved == true).ToList();
            }
            if (type == "Open")
            {
                return _postRepository.TableNoTracking.Include(p => p.Z_Harag_Photo).Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.Id == postId && p.IsClosed == false && p.IsDispayed == false && p.IsAnswered == true).ToList();
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
            var post = _postRepository.Table.Where(p => p.Id == postId).FirstOrDefault();
            if (post != null)
            {

                return _postRepository.TableNoTracking
                    .Include(p => p.Z_Harag_Photo)
                    .Include(p => p.Customer)
                    .Include(p => p.Category)
                    .Include(p => p.City)
                    .Where(p => p.Id == postId && p.IsDispayed == true).FirstOrDefault(); 
            }
            return null;
        }

        public List<Z_Harag_Post> GetFeaturedPosts()
        { 
                return _postRepository.TableNoTracking
                    .Include(p => p.Z_Harag_Photo)
                    .Include(p => p.Customer)
                    .Include(p => p.Category)
                    .Include(p => p.City)
                    .Where(p => p.IsDispayed == true).OrderBy(r => r.DateCreated).ToList();
            
        }

        public List<City> GetCities()
        {
            var query = _cityRepository.TableNoTracking.ToList();
            return query;
        }


        #endregion






    }
}