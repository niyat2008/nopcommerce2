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
using Nop.Core.Data;
using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Localization;
using Nop.Services.Z_Consultant.Helpers;
using static Nop.Services.Z_Consultant.Post.PostService;

namespace Nop.Services.Z_Consultant.Comment
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Z_Consultant_Comment> _commentRepository;
        ILocalizationService _localizationService;
        private readonly IHostingEnvironment _env;

        public CommentService(IRepository<Z_Consultant_Comment> commentRepository,
            ILocalizationService localizationService,
            IHostingEnvironment env
            )
        {
            this._localizationService = localizationService;
            this._env = env;
            this._commentRepository = commentRepository;
        }

        public Z_Consultant_Comment AddCommentByConsultant(CommentForPostModel CommentForPostDto, int consultantId, IList<string> files, List<string> errors)
        {
            List<KeyAndValue> filesUrl = UploadFiles(files, errors);

            if (errors.Count > 0)
            {
                return null;
            }
            var comment = new Z_Consultant_Comment()
            {
                Text = CommentForPostDto.Text,
                PostId = CommentForPostDto.PostId,
                ConsultantId = consultantId,
                CommentedBy = CommentByTypes.Consultant,
                DateCreated = DateTime.Now,
            };
            foreach (var item in filesUrl)
            {
                if (item.Key.ToLower() == "image")
                    comment.Photos.Add(new Z_Consultant_CommentPhoto()
                    {
                        Url = item.Value
                    });
                //else if (item.Key.ToLower() == "video")
                //    post.Videos.Add(new Video()
                //    {
                //        Url = item.Value
                //    });
            }
            _commentRepository.Insert(comment);
            return comment;
        }

        public Z_Consultant_Comment AddCommentByCustomer(CommentForPostModel CommentForPostDto, int customerId, IList<string> files, List<string> errors)
        {
            List<KeyAndValue> filesUrl = UploadFiles(files, errors);

            if (errors.Count > 0)
            {
                return null;
            }
            var comment = new Z_Consultant_Comment()
            {
                Text = CommentForPostDto.Text,
                PostId = CommentForPostDto.PostId,
                CustomerId = customerId,
                CommentedBy = CommentByTypes.Registered,
                DateCreated = DateTime.Now
            };
            foreach (var item in filesUrl)
            {
                if (item.Key.ToLower() == "image")
                    comment.Photos.Add(new Z_Consultant_CommentPhoto()
                    {
                        Url = item.Value
                    });
                //else if (item.Key.ToLower() == "video")
                //    post.Videos.Add(new Video()
                //    {
                //        Url = item.Value
                //    });
            }
            _commentRepository.Insert(comment);
            return comment;
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
                    filesUrl.Add(new Post.PostService.KeyAndValue() { Key = "image", Value = fileName });
                }
            }

            return filesUrl;
        }



        public void DeleteComment(DeleteCommentModel DeleteCommentModel)
        {
            var comment = _commentRepository.Table
                .Where(c => c.Id == DeleteCommentModel.CommentId).FirstOrDefault();
            if (comment != null)
                _commentRepository.Delete(comment);
        }

        public Z_Consultant_Comment GetComment(int commentId)
        {
            var comment = _commentRepository.TableNoTracking
                .Include(c => c.Customer).Include(c => c.Consultant)
                .Where(c => c.Id == commentId).FirstOrDefault();
            return comment;

        }

        public Z_Consultant_Comment GetCommentByCommentIdAndConsultantId(int commentId, int consultantId)
        {
            var comment = _commentRepository.TableNoTracking
                .Include(c => c.Consultant).Where(c => c.Id == commentId && c.ConsultantId == consultantId)
                .FirstOrDefault();

            return comment;
        }

        public Z_Consultant_Comment GetCommentByCommentIdAndCustomerId(int commentId, int customerId)
        {
            var comment = _commentRepository.TableNoTracking
                .Include(c => c.Customer).Where(c => c.Id == commentId && c.CustomerId == customerId)
                .FirstOrDefault();

            return comment;
        }

        public PagedList<Z_Consultant_Comment> GetCommentsByPostId(PagingParams pagingParams, int postId)
        {
            var query = _commentRepository.TableNoTracking
                .Include(c => c.Customer).Include(c => c.Consultant)
                .Where(c => c.PostId == postId)
                .OrderBy(c => c.DateCreated);

            query = query.OrderBy(c => c.DateCreated);
            return new PagedList<Z_Consultant_Comment>(
                query, pagingParams.PageNumber, pagingParams.PageSize,true);
        }

        public int GetPostIdByCommentId(int commentId)
        {
            return _commentRepository.TableNoTracking
                .Where(c => c.Id == commentId).Select(c => c.PostId).FirstOrDefault();
        }

        public bool IsCommentPostClosed(int CommentId)
        {
             var comment= _commentRepository.TableNoTracking
                .Include(c => c.Post).Where(c => c.Id == CommentId).FirstOrDefault();
            if(comment!=null)
            {
                return comment.Post.IsClosed;
            }
            return false;

        }

        public bool IsConsultantAuthToComment(int CommentId, int ConsultantId)
        {
            var comment = _commentRepository.TableNoTracking
                .Where(c => c.Id == CommentId && c.ConsultantId == ConsultantId).FirstOrDefault();

            if (comment == null)
                return false;

            return true;
        }

        public bool IsCustomerAuthToComment(int CommentId, int CustomerId)
        {
            var comment = _commentRepository.TableNoTracking
                .Where(c => c.Id == CommentId && c.CustomerId == CustomerId).FirstOrDefault();

            if (comment == null)
                return false;

            return true;
        }

        public bool IsExist(int CommentId)
        {
            var comment = _commentRepository.TableNoTracking.Where(c => c.Id == CommentId).FirstOrDefault();
            if (comment == null)
                return false;
            else
                return true;
        }

        public Z_Consultant_Comment UpdateComment(UpdateCommentModel UpdateCommentModel)
        {
            var comment = _commentRepository.Table.Where(c => c.Id == UpdateCommentModel.CommentId).FirstOrDefault();
            comment.Text = UpdateCommentModel.Text;
            _commentRepository.Update(comment);
            return comment;
        }
    }
}
