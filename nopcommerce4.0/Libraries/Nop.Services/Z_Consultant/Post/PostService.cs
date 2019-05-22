using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using Nop.Core.Domain.Z_Consultant;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Z_Consultant.Helpers;

namespace Nop.Services.Z_Consultant.Post
{
    
    public class PostService : IPostService
    {

        private readonly IRepository<Z_Consultant_Post> _postRepository;
        private readonly IRepository<Z_Consultant_Category> _categoryRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Neighborhood> _neighborhoodRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;
        ILocalizationService _localizationService;


        public PostService(IRepository<Z_Consultant_Post> postRepository,
            IRepository<Z_Consultant_Category> categoryRepository,
            ILocalizationService localizationService,
            IHostingEnvironment env, IRepository<City> _cityRepository,
            IRepository<Neighborhood> _neighborhoodRepository,
        IEventPublisher eventPublisher)
        {
            this._postRepository = postRepository;
            this._categoryRepository = categoryRepository;
            this._eventPublisher = eventPublisher;
            this._env = env;
            this._localizationService = localizationService;
            this._cityRepository = _cityRepository;
            this._neighborhoodRepository = _neighborhoodRepository;
        }


        public class KeyAndValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public class ImageBytesAndFormat
        {
            public byte[] ImageBytes { get; set; }
            public string ImageFormateName { get; set; }
        }


        public  Z_Consultant_Post AddNewPost(PostForPostModel postForPostModel, int cutomerId, IList<string> files, List<string> errors)
        {
            List<KeyAndValue> filesUrl =  UploadFiles(files, errors);

            if (errors.Count > 0)
            {
                return null;
            }

            var post = new Z_Consultant_Post()
            {
                Text = postForPostModel.Text,
                Title= postForPostModel.Title,
                CategoryId = postForPostModel.CategoryId,
                CustomerId = cutomerId,
                DateCreated = DateTime.Now,
                IsAnswered = false,
                IsClosed = false,
                IsDispayed=false,
                IsReserved=false,
                IsCommon = false
            };
            foreach (var item in filesUrl)
            {
                if (item.Key.ToLower() == "image")
                    post.Photos.Add(new Z_Consultant_Photo()
                    {
                        Url = item.Value
                    });
                //else if (item.Key.ToLower() == "video")
                //    post.Videos.Add(new Video()
                //    {
                //        Url = item.Value
                //    });
            }


            _postRepository.Insert(post);
            return post;
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

            var ImagesPath = Path.Combine(_env.WebRootPath, "ConsultantApi\\Uploads\\Images");
            var logoImage = Path.Combine(_env.WebRootPath, "images\\thumbs\\watermark.png");

            //var Vedios = Path.Combine(_env.WebRootPath, "Uploads\\Videos"); /images/Consultant/images/logo3.png

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


        private Bitmap DrawLogo(string logo, Image img)
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





        public PagedList<Z_Consultant_Post> GetClosedPosts(PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer).Include(p => p.Photos).Where(p => p.IsClosed == true && p.IsDispayed==true);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedPostsAdmin(PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer).Where(p => p.IsClosed == true);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedPostsByCategoryId(PagingParams pagingParams, int categoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.IsClosed == true && p.IsDispayed==true && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }
        public PagedList<Z_Consultant_Post> GetClosedPostsByCategoryIdAdmin(PagingParams pagingParams, int categoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.IsClosed == true &&  (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedPostsBySubCategoryId(PagingParams pagingParams, int subCategoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.IsClosed == true && p.IsDispayed==true && p.SubCategoryId == subCategoryId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedPostsBySubCategoryIdAdmin(PagingParams pagingParams, int subCategoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.IsClosed == true &&  p.SubCategoryId == subCategoryId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedPostsForCustomer(PagingParams pagingParams, int customerId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.CustomerId == customerId && p.IsClosed == true);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedPostsForCustomerByCategoryId(PagingParams pagingParams, int categoryId, int customerId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == true && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId) && p.CustomerId == customerId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedPostsForCustomerBySubCategoryId(PagingParams pagingParams, int subCategoryId, int customerId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == true && p.SubCategoryId == subCategoryId && p.CustomerId == customerId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsForCustomer(PagingParams pagingParams, int customerId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.CustomerId == customerId && p.IsClosed == false);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsForCustomerByCategoryId(PagingParams pagingParams, int categoryId, int customerId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.IsClosed == false && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId) && p.CustomerId == customerId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsForCustomerBySubCategoryId(PagingParams pagingParams, int subCategoryId, int customerId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.IsClosed == false && p.SubCategoryId == subCategoryId && p.CustomerId == customerId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetPostsForCustomer(PagingParams pagingParams, int customerId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.CustomerId == customerId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetPostsForCustomerByCategoryId(PagingParams pagingParams, int categoryId, int customerId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId) && p.CustomerId == customerId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetPostsForCustomerBySubCategoryId(PagingParams pagingParams, int subCategoryId, int customerId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.SubCategoryId == subCategoryId && p.CustomerId == customerId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public bool CloseAndRatePost(CloseAndRatePostModel closeAndRatePostDto, int customerId)
        {
            var atLeastSingleChange = false;

            var post = _postRepository.Table.Where(p => p.Id == closeAndRatePostDto.Id).FirstOrDefault();

            if (!post.IsClosed && closeAndRatePostDto.IsColsed == true)
            {
                post.IsClosed = true;
                atLeastSingleChange = true;
            }

            if (post.Rate == null && closeAndRatePostDto.Rate != null)
            {
                post.Rate = closeAndRatePostDto.Rate;
                atLeastSingleChange = true;
            }

            if (atLeastSingleChange)
                _postRepository.Update(post);

            return atLeastSingleChange;
        }

        public bool IsCustomerAuthToPost(int postId, int customerId)
        {
            var post = _postRepository.TableNoTracking
                .Where(p => p.Id == postId && p.CustomerId == customerId)
                .FirstOrDefault();
             
            if (post == null)
                return false;

            return true;
        }


        public PagedList<Z_Consultant_Post> GetOpenPostsNotAns(PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false && p.IsReserved==false && p.IsAnswered == false);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsNotAnsByCategoryId(PagingParams pagingParams, int categoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false && p.IsReserved==false && p.IsAnswered == false && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));

            query = query.OrderBy(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsNotAnsBySubCategoryId(PagingParams pagingParams, int subCategoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false && p.IsReserved==false && p.IsAnswered == false && p.SubCategoryId == subCategoryId);

            query = query.OrderBy(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsForConsultant(PagingParams pagingParams, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.ConsultantId == consultantId && p.IsAnswered==true && p.IsClosed == false);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsForConsultantByCategoryId(PagingParams pagingParams, int categoryId, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false && p.IsAnswered==true && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId) && p.ConsultantId == consultantId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsForConsultantBySubCategoryId(PagingParams pagingParams, int subCategoryId, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false && p.IsAnswered==true && p.SubCategoryId == subCategoryId && p.ConsultantId == consultantId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedPostsForConsultant(PagingParams pagingParams, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.ConsultantId == consultantId && p.IsReserved==true && p.IsClosed == true);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedPostsForConsultantByCategoryId(PagingParams pagingParams, int categoryId, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == true && p.IsReserved==true && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId) && p.ConsultantId == consultantId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedPostsForConsultantBySubCategoryId(PagingParams pagingParams, int subCategoryId, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == true&& p.IsReserved==true && p.SubCategoryId == subCategoryId && p.ConsultantId == consultantId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetPostsForConsultant(PagingParams pagingParams, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.CustomerId == consultantId && p.IsReserved==true);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetPostsForConsultantByCategoryId(PagingParams pagingParams, int categoryId, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId) && p.ConsultantId == consultantId && p.IsReserved==true);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetPostsForConsultantBySubCategoryId(PagingParams pagingParams, int subCategoryId, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.SubCategoryId == subCategoryId && p.ConsultantId == consultantId && p.IsReserved==true);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public bool IsConsultantAuthToPost(int postId, int consultantId)
        {
            var post = _postRepository.TableNoTracking
                .Where(p => p.Id == postId && p.ConsultantId == consultantId && p.IsReserved==true).FirstOrDefault();

            if (post == null)
                return false;

            return true;
        }

        public bool IsExist(int postId)
        {
            var post = _postRepository.TableNoTracking.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
                return false;
            else
                return true;
        }

        public bool IsCategoryConatinTheSub(SetPostToSubCategoryModel setPostToSubCategory)
        {
            var categoryId = _postRepository.TableNoTracking
                .Where(p => p.Id == setPostToSubCategory.PostId).Select(p => p.CategoryId).FirstOrDefault();
            var listOfSub = _categoryRepository.TableNoTracking
                .Where(c => c.Id == categoryId).Select(c => c.SubCategories).ToList();
            foreach (var sub in listOfSub[0])
            {
                if (sub.Id == setPostToSubCategory.SubCategoryId)
                    return true;
            }
            return false;
        }

        public void SetPostToSubCategory(SetPostToSubCategoryModel setPostToSubCategory)
        {
            var post = _postRepository.Table
                .Where(p => p.Id == setPostToSubCategory.PostId && p.IsReserved==true).FirstOrDefault();
            if (post != null)
            {
                post.IsSetToSubCategory = true;
                post.SubCategoryId = setPostToSubCategory.SubCategoryId;
                _postRepository.Update(post);
            }
        }

        public void SetPostToCategory(SetPostToCategoryModel setPostToCategory)
        {
            var post = _postRepository.Table
                .Where(p => p.Id == setPostToCategory.PostId && p.IsReserved==true).FirstOrDefault();


            if (post != null)
            {
                post.CategoryId = setPostToCategory.CategoryId;
                post.IsSetToSubCategory = false;
                _postRepository.Update(post);
            }
        }

        public bool IsClosed(int postId)
        {
            return _postRepository.TableNoTracking.Where(p => p.Id == postId).Select(p => p.IsClosed).FirstOrDefault();
        }

        public bool IsAnswered(int postId)
        {
            return _postRepository.TableNoTracking.Where(p => p.Id == postId).Select(p => p.IsAnswered).FirstOrDefault();
        }

        public Z_Consultant_Post GetPost(int postId)
        {
            return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.Id == postId).FirstOrDefault();
        }

        public void SetPostAnsweredByConsultant(int postId, int consultantId)
        {
            var post = _postRepository.Table.Where(p => p.Id == postId).FirstOrDefault();
            post.IsAnswered = true;
            post.IsReserved = true;
            post.ConsultantId = consultantId;
            _postRepository.Update(post);
        }

        public void SetPostToCategoryAndSubCategory(SetPostToCategoryAndSubCategoryModel setPostToCategoryAndSub)
        {
            var post = _postRepository.Table
                .Where(p => p.Id == setPostToCategoryAndSub.PostId).FirstOrDefault();


            if (post != null)
            {
                post.CategoryId = setPostToCategoryAndSub.CategoryId;
                post.SubCategoryId = setPostToCategoryAndSub.SubCategoryId;
                post.IsSetToSubCategory = true;
                _postRepository.Update(post);
            }
        }

        public bool IsCategoryConatinTheSub(SetPostToCategoryAndSubCategoryModel setPostToCategoryAnSub)
        {
            
            var listOfSub = _categoryRepository.TableNoTracking
                .Where(c => c.Id == setPostToCategoryAnSub.CategoryId).Select(c => c.SubCategories).ToList();
            foreach (var sub in listOfSub[0])
            {
                if (sub.Id == setPostToCategoryAnSub.SubCategoryId)
                    return true;
            }
            return false;
        }

        public PagedList<Z_Consultant_Post> GetPosts(PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetPostsByCategoryId(PagingParams pagingParams, int categoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetPostsBySubCategoryId(PagingParams pagingParams, int subCategoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.SubCategoryId == subCategoryId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsAns(PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false && p.IsAnswered == true);

            query = query.OrderBy(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsAnsByCategoryId(PagingParams pagingParams, int categoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false && p.IsAnswered == true && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));

            query = query.OrderBy(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsAnsBySubCategoryId(PagingParams pagingParams, int subCategoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false && p.IsAnswered == true && p.SubCategoryId == subCategoryId);

            query = query.OrderBy(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPosts(PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false);

            query = query.OrderBy(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsByCategoryId(PagingParams pagingParams, int categoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false  && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));

            query = query.OrderBy(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetOpenPostsBySubCategoryId(PagingParams pagingParams, int subCategoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false  && p.SubCategoryId == subCategoryId);

            query = query.OrderBy(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedDisplayedPosts(PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == true);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedDisplayedPostsByCategoryId(PagingParams pagingParams, int categoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.IsClosed == true && p.IsDispayed == true && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedDisplayedPostsBySubCategoryId(PagingParams pagingParams, int subCategoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.IsClosed == true && p.IsDispayed == true && p.SubCategoryId == subCategoryId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }


        public PagedList<Z_Consultant_Post> GetClosedHiddenPosts(PagingParams pagingParams)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == false);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedHiddenPostsByCategoryId(PagingParams pagingParams, int categoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.IsClosed == true && p.IsDispayed == false && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetClosedHiddenPostsBySubCategoryId(PagingParams pagingParams, int subCategoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory)
                .Include(p => p.Customer)
                .Where(p => p.IsClosed == true && p.IsDispayed == false && p.SubCategoryId == subCategoryId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public bool IsReserved(int postId)
        {
            return _postRepository.TableNoTracking.Where(p => p.Id == postId).Select(p => p.IsReserved).FirstOrDefault();
        }

        public void ReservePost(int postId, int ConsultantId)
        {
            var post = _postRepository.Table.Where(p => p.Id == postId).FirstOrDefault();
            post.ConsultantId = ConsultantId;
            post.IsReserved = true;
            _postRepository.Update(post);
        }

        public void UnReservePost(int postId)
        {
            var post = _postRepository.Table.Where(p => p.Id == postId).FirstOrDefault();
            post.ConsultantId = null;
            post.IsReserved = false;
            _postRepository.Update(post);
        }

        public void DisplayPost(int PostId)
        {
            var post = _postRepository.Table.Where(p => p.Id == PostId).FirstOrDefault();
            if (post != null)
                post.IsDispayed = true;

            _postRepository.Update(post);
        }

        public void HidePost(int PostId)
        {
            var post = _postRepository.Table.Where(p => p.Id == PostId).FirstOrDefault();
            if (post != null)
                post.IsDispayed = false;
        }

        public PagedList<Z_Consultant_Post> SearchPosts(PagingParams pagingParams, SearchModel searchModel)
        {
            string[] words = searchModel.Term.Split(' ');
            var query = _postRepository.TableNoTracking
                .Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p=> p.IsClosed == true && p.IsDispayed==true   &&  (words.Any(word => p.Text.Contains(word)) || words.Any(word => p.Title.Contains(word))));

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,false);
        }

        //Get Post By Id
        public Z_Consultant_Post GetPostById(int postId)
        {
            if(postId !=0)
            {
                var post = _postRepository.TableNoTracking.Include(p => p.Customer).Include(p=>p.Consultant).FirstOrDefault(p=>p.Id==postId);

                return post;

               
            }
            return null;
        }

        ////Get Reserved Post By Id
        //public Z_Consultant_Post GetReservedPostById(int postId)
        //{
        //    if (postId != 0)
        //    {
        //        var post = _postRepository.TableNoTracking.Include(p => p.Customer).Include(p=>p.Consultant).FirstOrDefault(p=>p.IsReserved==true);

        //        return post;


        //    }
        //    return null;
        //}

        public bool ClosePost(ClosePostModel closePostDto, int customerId)
        {
            var atLeastSingleChange = false;

            var post = _postRepository.Table.Where(p => p.Id == closePostDto.Id).FirstOrDefault();

            if (!post.IsClosed)
            {
                post.IsClosed = true;
                atLeastSingleChange = true;
            }
            

            if (atLeastSingleChange)
                _postRepository.Update(post);

            return atLeastSingleChange;
        }

        public bool RatePost(RatePostModel ratePostDto, int customerId)
        {
            var atLeastSingleChange = false;

            var post = _postRepository.Table.Where(p => p.Id == ratePostDto.Id).FirstOrDefault();

            if (!post.IsClosed)
            {
                post.Rate = ratePostDto.Rate;
                atLeastSingleChange = true;
            }


            if (atLeastSingleChange)
                _postRepository.Update(post);

            return atLeastSingleChange;
        }

        public PagedList<Z_Consultant_Post> GetReservedPostsForConsultant(PagingParams pagingParams, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.ConsultantId == consultantId && p.IsReserved == true && p.IsAnswered == false && p.IsClosed == false);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetReservedPostsForConsultantByCategoryId(PagingParams pagingParams, int categoryId, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false && p.IsAnswered == false && p.IsReserved == true && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId) && p.ConsultantId == consultantId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public PagedList<Z_Consultant_Post> GetReservedPostsForConsultantBySubCategoryId(PagingParams pagingParams, int subCategoryId, int consultantId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsClosed == false && p.IsAnswered == false && p.IsReserved == true && p.SubCategoryId == subCategoryId && p.ConsultantId == consultantId);

            query = query.OrderByDescending(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public bool IsRated(int postId)
        {
            var post = _postRepository.TableNoTracking.Where(p => p.Id == postId).FirstOrDefault();
            if (post != null)
            {
                if (post.Rate >0 && post.Rate<6)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public PagedList<Z_Consultant_Post> GetCommonPostsForCustomer(PagingParams pagingParams, int currentUserId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer)
                .Include(p => p.Category).Include(p => p.SubCategory)
                .Where(p => p.IsCommon == true);

            query = query.OrderBy(p => p.DateCreated);

            return new PagedList<Z_Consultant_Post>(
                query, pagingParams.PageNumber, pagingParams.PageSize, true);
        }




        /// <summary>
        ///  For Shipping Address 
        /// </summary>
        /// <returns></returns>

        public List<City> GetCities()
        {
            var query = _cityRepository.TableNoTracking;
            return query.ToList();
        }

        public List<Neighborhood> GetCityNeighborhood(int cityId)
        {
            var query = _neighborhoodRepository.TableNoTracking;

            query = query.Where(m => m.CityId == cityId);

            return query.ToList();
        }

        public bool UpdatePostTitle(int postId, string text)
        {
            var post = _postRepository.Table.Where(m => m.Id == postId).FirstOrDefault();

            post.Title = text;

            _postRepository.Update(post);

            return true;
        }

        public bool UpdatePostText(int postId, string text)
        {
            var post = _postRepository.Table.Where(m => m.Id == postId).FirstOrDefault();

            post.Text = text;

            _postRepository.Update(post);

            return true;
        }
    }
}
