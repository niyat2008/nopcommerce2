using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Consultant.Api.Models.Login
{
    public class LoginModel
    {
        [Required]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "You must specify a password 12 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Must be Digits")]
        public string Phone { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}
