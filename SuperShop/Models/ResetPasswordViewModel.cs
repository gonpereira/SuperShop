using System.ComponentModel.DataAnnotations;

namespace SuperShop.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string UserName { get; set; }


        [Required, DataType(DataType.Password)]
        public string Password { get; set; }



        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not equal..")]
        public string ConfirmPassword { get; set; }



        [Required]
        public string Token { get; set; }
    }
}
