﻿using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using Nop.Web.Validators.Customer;

namespace Nop.Web.Models.Customer
{
    [Validator(typeof(LoginValidator))]
    public partial class LoginModel : BaseNopModel
    {
        public bool CheckoutAsGuest { get; set; }

        //[DataType(DataType.EmailAddress)]
        //[NopResourceDisplayName("Account.Login.Fields.Email")]
        //public string Email { get; set; }


        [DataType(DataType.PhoneNumber)]
        [NopResourceDisplayName("Account.Login.Fields.Mobile")]
        public string Mobile { get; set; }



        public bool UsernamesEnabled { get; set; }
        [NopResourceDisplayName("Account.Login.Fields.UserName")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [NoTrim]
        [NopResourceDisplayName("Account.Login.Fields.Password")]
        public string Password { get; set; }

        [NopResourceDisplayName("Account.Login.Fields.RememberMe")]
        public bool RememberMe { get; set; }

        public bool DisplayCaptcha { get; set; }
    }
}