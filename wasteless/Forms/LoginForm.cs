using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wasteless.Forms
{
    public class LoginForm
    {
        [Required(ErrorMessage = "Email required."), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required.")]
        public string Password { get; set; }
    }
}