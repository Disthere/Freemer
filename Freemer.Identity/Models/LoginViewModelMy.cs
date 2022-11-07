using System.ComponentModel.DataAnnotations;

namespace Freemer.Identity.Models
{
    public class LoginViewModelMy
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
