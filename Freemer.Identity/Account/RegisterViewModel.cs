using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Freemer.Identity.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        //[Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Минимальная длина должна быть больше 6 символов (цифры и буквы в любом регистре)!")]
        [MaxLength(50, ErrorMessage = "Максимальная длина должна быть не больше 50 символов (цифры и буквы в любом регистре)!")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
