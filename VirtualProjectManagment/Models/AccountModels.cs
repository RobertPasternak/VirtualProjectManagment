using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Weborb.Service;

namespace VirtualProjectManagment.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Hasło")]
        public string Password { get; set; }     
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Błędny adres {0}.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "{0} musi mieć co najmniej {1} znaków długości.")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Potwierdź Hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }
    }
}