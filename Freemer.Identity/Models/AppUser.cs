using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Freemer.Identity.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[Range(15,100,ErrorMessage ="Недопустимый возраст")]
        //public int Age { get; set; }
    }
}
