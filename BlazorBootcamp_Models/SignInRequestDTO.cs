using System.ComponentModel.DataAnnotations;

namespace BlazorBootcamp_Models
{
    public class SignInRequestDTO
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email adress")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
