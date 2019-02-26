using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Z_HaragAdmin.Customers;
using Microsoft.AspNetCore.Hosting;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Harag.Helpers;
using Nop.Web.Models.HaragAdmin.Customers;
using Nop.Services.Z_HaragAdmin;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class CustomerController : Controller
    {
        #region Fields
        private readonly ICustomerService _customerService;
        //private readonly IPostService _postService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        #endregion
        #region Ctor
        public CustomerController(ICustomerService customerService, Core.IWorkContext workContext, IHostingEnvironment env)
        {
            this._customerService = customerService;
            
            this._workContext = workContext;
            this._env = env;
        }
        #endregion
        #region Methods
        //Get Members
        public ActionResult HaragGetMembers()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            return View("~/Themes/Pavilion/Views/HaragAdmin/Customer/GetCustomers.cshtml");
        }
        //Get Members Ajax
        [HttpPost]
        public ActionResult GetHaragCustomerAjax()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            //Server Side Parameters
            int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];



            var customersInDb = _customerService.GetMembers(start, length, searchValue, sortColumnName, sortDirection);

            var customers = new CustomersOutputModel
            {
                Customers = customersInDb.Select(m => new CustomersModel
                {
                    Id=m.Id,
                    Username = m.Username,
                    Email = m.Email,
                    Mobile = m.Mobile,
                    Blocked=m.Blocked
                    



                }).ToList()
            };

            return Json(new { data = customers.Customers });
        }

        //Get Member Details
        //public ActionResult HaragGetMemberDetails(int id)
        //{
        //    if (id==null || id==0)
        //    {
        //        return NotFound();
        //    }
        //    var customerInDb = _customerService.GetMemberDetails(id);
        //    var customer = new CustomersModel
        //    {
        //        //Id=customerInDb.Id,
        //        Id = customerInDb.Id,
        //        Username = customerInDb.Username,
        //        Email = customerInDb.Email,
        //        Mobile = customerInDb.Mobile
               
        //    };

        //    return View("~/Themes/Pavilion/Views/ConsultantAdmin/Customers/GetCustomerDetails.cshtml", customer);
        //}

        //Get Online Members
        //public ActionResult GetOnlineMembers()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    return View("~/Themes/Pavilion/Views/ConsultantAdmin/Customers/GetOnlineMembers.cshtml");
        //}

      
        ////Get Online Members Ajax
        //[HttpPost]
        //public ActionResult GetOnlineMembersAjax()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();


        //    //Server Side Parameters
        //    int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

        //    int length = Convert.ToInt32(Request.Form["length"]);
        //    string searchValue = Request.Form["search[value]"];
        //    string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
        //    string sortDirection = Request.Form["order[0][dir]"];



        //    var membersInDb = _customerService.GetOnlineMembers(start, length, searchValue, sortColumnName, sortDirection);

        //    var members = new CustomersOutputModel
        //    {
        //        Customers = membersInDb.Select(m => new CustomersModel
        //        {
        //            Id=m.Id,
        //            Username = m.Username,
        //            Email = m.Email,
        //            Mobile = m.Mobile



        //        }).ToList()
        //    };

        //    return Json(new { data = members.Customers });
        //}

      

        ////Get Consultants
        //public ActionResult GetConsultants()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    return View("~/Themes/Pavilion/Views/ConsultantAdmin/Customers/GetConsultants.cshtml");
        //}

        ////Get Consultants Ajax
        //[HttpPost]
        //public ActionResult GetConsultantsAjax()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();


        //    //Server Side Parameters
        //    int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

        //    int length = Convert.ToInt32(Request.Form["length"]);
        //    string searchValue = Request.Form["search[value]"];
        //    string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
        //    string sortDirection = Request.Form["order[0][dir]"];



        //    var consultantsInDb = _customerService.GetConsultants(start, length, searchValue, sortColumnName, sortDirection);

        //    var consultants = new CustomersOutputModel
        //    {
        //        Customers = consultantsInDb.Select(m => new CustomersModel
        //        {
        //            Id=m.Id,
        //            Username = m.Username,
        //            Email = m.Email,
        //            Mobile = m.Mobile



        //        }).ToList()
        //    };

        //    return Json(new { data = consultants.Customers });
        //}

        ////Get Online Consultant
        //public ActionResult GetOnlineConsultants()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    return View("~/Themes/Pavilion/Views/ConsultantAdmin/Customers/GetOnlineConsultants.cshtml");
        //}

        ////Get Online Consultant Ajax
        //[HttpPost]
        //public ActionResult GetOnlineConsultantAjax()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();


        //    //Server Side Parameters
        //    int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

        //    int length = Convert.ToInt32(Request.Form["length"]);
        //    string searchValue = Request.Form["search[value]"];
        //    string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
        //    string sortDirection = Request.Form["order[0][dir]"];



        //    var consultantInDb = _customerService.GetOnlineConsultants(start, length, searchValue, sortColumnName, sortDirection);

        //    var consultant = new CustomersOutputModel
        //    {
        //        Customers = consultantInDb.Select(m => new CustomersModel
        //        {
        //            Id=m.Id,
        //            Username = m.Username,
        //            Email = m.Email,
        //            Mobile = m.Mobile,
        //            Active=m.Active



        //        }).ToList()
        //    };

        //    return Json(new { data = consultant.Customers });
        //}

        ////Get Consultant Details
        //public ActionResult GetConsultantDetails(int id)
        //{
        //    if (id==null || id==0)
        //    {
        //        return NotFound();
        //    }

        //    var consultantInDb = _customerService.GetMemberDetails(id);

        //    var customer = new CustomersModel
        //    {
        //        //Id=customerInDb.Id,
        //        Id = consultantInDb.Id,
        //        Username = consultantInDb.Username,
        //        Email = consultantInDb.Email,
        //        Mobile = consultantInDb.Mobile,
        //        Active = consultantInDb.Active,
        //        LastIpAddress = consultantInDb.LastIpAddress,
        //        LastActivityDateUtc = consultantInDb.LastActivityDateUtc
        //    };

        //    return View("~/Themes/Pavilion/Views/ConsultantAdmin/Customers/GetConsultantDetails.cshtml", customer);
        //}

        ////Get Customer Posts 
        
        //public ActionResult GetCustomerPosts(int customerId)
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    var model = new CustomerPostsModel
        //    {
        //        Id = customerId
        //    };

        //    return View("~/Themes/Pavilion/Views/ConsultantAdmin/Customers/GetCustomerPosts.cshtml",model);
        //}

        ////Get Customer Posts In Json 
        //[HttpPost]
        //public ActionResult GetCustomerPostsInJson(int customerId,string type,DateTime? from,DateTime? to)
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    if (customerId == null || customerId == 0)
        //        return NotFound();

        //    //Server Side Parameters
        //    int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

        //    int length = Convert.ToInt32(Request.Form["length"]);
        //    string searchValue = Request.Form["search[value]"];
        //    string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
        //    string sortDirection = Request.Form["order[0][dir]"];

        //    var customerPostsInDb = _postService.GetCustomerPosts(customerId, type, start, length, searchValue, sortColumnName, sortDirection, from, to);

        //    var customerPosts = new CustomerPostsOutputModel
        //    {
        //        CustomerPosts=customerPostsInDb.Select(m=>new CustomerPostsModel
        //        {
        //            Username = m.Customer.Username,
        //            Email = m.Customer.Email,
        //            Mobile = m.Customer.Mobile,
        //            SystemName=m.Customer.SystemName,
        //            PostId = m.Id,
        //            Text = m.Text,
        //            Title = m.Title,
        //            DateCreated = m.DateCreated,
        //            DateUpdated = m.DateUpdated,
        //            IsClosed = m.IsClosed,
        //            IsAnswered = m.IsAnswered,
        //            Rate = m.Rate,
        //            IsDispayed = m.IsDispayed,
        //            IsReserved = m.IsReserved,
        //            IsSetToSubCategory = m.IsSetToSubCategory,
        //            SubCategoryId = m.SubCategoryId,
        //            CategoryId = m.CategoryId,
        //            PostOwner = m.Customer.Username,
        //            CategoryName = m.Category.Name,
        //            SubCategoryName = m.SubCategory?.Name

        //        }).ToList()
        //    };
            
        //    return Json(new { data = customerPosts.CustomerPosts });
        //}

        ////Get Customer Post By PostId and CustomerId
        //public ActionResult GetCustomerPostById(int customerId,int postId,string type)
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    if (customerId == null || customerId == 0 || postId == null || postId == 0)
        //        return NotFound();

        //    var postInDb = _postService.GetCustomerPostById(customerId, postId, type);

        //    var customerPost = new CustomerPostsOutputModel
        //    {
        //        CustomerPosts = postInDb.Select(m => new CustomerPostsModel
        //        {
        //            Username = m.Customer.Username,
        //            Email = m.Customer.Email,
        //            Mobile = m.Customer.Mobile,
        //            SystemName = m.Customer.SystemName,
        //            PostId = m.Id,
        //            Text = m.Text,
        //            Title = m.Title,
        //            DateCreated = m.DateCreated,
        //            DateUpdated = m.DateUpdated,
        //            IsClosed = m.IsClosed,
        //            IsAnswered = m.IsAnswered,
        //            Rate = m.Rate,
        //            IsDispayed = m.IsDispayed,
        //            IsReserved = m.IsReserved,
        //            IsSetToSubCategory = m.IsSetToSubCategory,
        //            SubCategoryId = m.SubCategoryId,
        //            CategoryId = m.CategoryId,
        //            PostOwner = m.Customer.Username,
        //            CategoryName = m.Category.Name,
        //            SubCategoryName = m.SubCategory?.Name

        //        }).ToList()
        //    };

        //    return Json(new { data = customerPost.CustomerPosts });
        //}

        ////Get Consultant Posts 

        //public ActionResult GetConsultantPosts(int consultantId)
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    var model = new CustomerPostsModel
        //    {
        //        Id = consultantId
        //    };

        //    return View("~/Themes/Pavilion/Views/ConsultantAdmin/Customers/GetConsultantPosts.cshtml", model);
        //}

        ////Get Consultant Posts In Json 
        //[HttpPost]
        //public ActionResult GetConsultantPostsInJson(int consultantId, string type, DateTime? from, DateTime? to)
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    if (consultantId == null || consultantId == 0)
        //        return NotFound();

        //    //Server Side Parameters
        //    int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

        //    int length = Convert.ToInt32(Request.Form["length"]);
        //    string searchValue = Request.Form["search[value]"];
        //    string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
        //    string sortDirection = Request.Form["order[0][dir]"];

        //    var customerPostsInDb = _postService.GetConsultantPosts(consultantId, type, start, length, searchValue, sortColumnName, sortDirection, from, to);

        //    var customerPosts = new CustomerPostsOutputModel
        //    {
        //        CustomerPosts = customerPostsInDb.Select(m => new CustomerPostsModel
        //        {
        //            Username = m.Consultant?.Username,
        //            Email = m.Consultant?.Email,
        //            Mobile = m.Consultant?.Mobile,
        //            SystemName = m.Consultant?.SystemName,
        //            PostId = m.Id,
        //            Text = m.Text,
        //            Title = m.Title,
        //            DateCreated = m.DateCreated,
        //            DateUpdated = m.DateUpdated,
        //            IsClosed = m.IsClosed,
        //            IsAnswered = m.IsAnswered,
        //            Rate = m.Rate,
        //            IsDispayed = m.IsDispayed,
        //            IsReserved = m.IsReserved,
        //            IsSetToSubCategory = m.IsSetToSubCategory,
        //            SubCategoryId = m.SubCategoryId,
        //            CategoryId = m.CategoryId,
        //            PostOwner = m.Consultant?.Username,
        //            CategoryName = m.Category.Name,
        //            SubCategoryName = m.SubCategory?.Name

        //        }).ToList()
        //    };

        //    return Json(new { data = customerPosts.CustomerPosts });
        //}

        ////Get Consultant Post By PostId and ConsultantId
        //public ActionResult GetConsultantPostById(int consultantId, int postId, string type)
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    if (consultantId == null || consultantId == 0 || postId == null || postId == 0)
        //        return NotFound();

        //    var postInDb = _postService.GetConsultantPostById(consultantId, postId, type);

        //    var customerPost = new CustomerPostsOutputModel
        //    {
        //        CustomerPosts = postInDb.Select(m => new CustomerPostsModel
        //        {
        //            Username = m.Consultant.Username,
        //            Email = m.Consultant.Email,
        //            Mobile = m.Consultant.Mobile,
        //            SystemName = m.Consultant.SystemName,
        //            PostId = m.Id,
        //            Text = m.Text,
        //            Title = m.Title,
        //            DateCreated = m.DateCreated,
        //            DateUpdated = m.DateUpdated,
        //            IsClosed = m.IsClosed,
        //            IsAnswered = m.IsAnswered,
        //            Rate = m.Rate,
        //            IsDispayed = m.IsDispayed,
        //            IsReserved = m.IsReserved,
        //            IsSetToSubCategory = m.IsSetToSubCategory,
        //            SubCategoryId = m.SubCategoryId,
        //            CategoryId = m.CategoryId,
        //            PostOwner = m.Customer.Username,
        //            CategoryName = m.Category.Name,
        //            SubCategoryName = m.SubCategory?.Name

        //        }).ToList()
        //    };

        //    return Json(new { data = customerPost.CustomerPosts });
        //}


        ////Get Top 20 Member By Posts number 
        //public ActionResult GetTop20MemberByPostsNumber()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();
            
        //    return View("~/Themes/Pavilion/Views/ConsultantAdmin/Customers/GetTop20MemberByPostNumber.cshtml");
        //}
        ////Get Top 20 Member By Posts number In Json
        //public ActionResult GetTop20MemberByPostsNumberInJson()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    var MembersInDb = _postService.GetTop20MemberByPostsNumber();
        //    //var Members = new Top20MemberOutputModel
        //    //{
        //    //    Customers = MembersInDb.Select(m => new Top20CustomerByPostsNumber
        //    //    {
        //    //        Id=MembersInDb.FirstOrDefault().CustomerId,
        //    //        Name=MembersInDb.FirstOrDefault().Customer?.SystemName,
        //    //        Email=MembersInDb.FirstOrDefault().Customer?.Email,
        //    //        Phone = MembersInDb.FirstOrDefault().Customer?.Mobile,
        //    //        PostsCount =MembersInDb.Count(),


        //    //    }).ToList()

        //   // };

        //    return Json(new { data = MembersInDb });

        //}
        ////Get Top 20 Consultant By Posts number 
        //public ActionResult GetTop20ConsultantByPostsNumber()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    return View("~/Themes/Pavilion/Views/ConsultantAdmin/Customers/GetTop20ConsultantByPostNumber.cshtml");
        //}
        ////Get Top 20 Member By Posts number In Json
        //public ActionResult GetTop20ConsultantByPostsNumberInJson()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    var ConsultantsInDb = _postService.GetTop20ConsultantByPostsNumber();
        //    //var Consultants = new Top20MemberOutputModel
        //    //{
        //    //    Customers = ConsultantsInDb.Select(m => new Top20CustomerByPostsNumber
        //    //    {
        //    //        Id = ConsultantsInDb.FirstOrDefault().Id,
        //    //        Name = ConsultantsInDb.FirstOrDefault().Consultant?.SystemName,
        //    //        Email = ConsultantsInDb.FirstOrDefault().Consultant?.Email,
        //    //        Phone= ConsultantsInDb.FirstOrDefault().Consultant?.Mobile,
        //    //        PostsCount = ConsultantsInDb.Count(),


        //    //    }).ToList()

        //    //};

        //    return Json(new { data = ConsultantsInDb });
        //}

        ////Get Top 20 Consultant By Posts number 
        //public ActionResult GetTop20ConsultantByPostsEvaluation()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    return View("~/Themes/Pavilion/Views/ConsultantAdmin/Customers/GetTop20ConsultantByPostEvaluate.cshtml");
        //}
        ////Get Top 20 Member By Posts number In Json
        //public ActionResult GetTop20ConsultantByPostsEvaluationInJson()
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    var ConsultantsInDb = _postService.GetTop20ConsultantByPostsNumber();
            

        //    return Json(new { data = ConsultantsInDb });
        //}

        ////Delete Customer
        //[HttpDelete]
        //public ActionResult DeleteMember(int id)
        //{
        //    if (id==null || id==0)
        //        return NotFound();
        //    _customerService.DeleteMember(id);
        //    return Json(new { result = true });
        //}
        #endregion

    }
}