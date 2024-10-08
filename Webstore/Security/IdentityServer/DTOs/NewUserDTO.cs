using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTOs
{
    public class NewUserDTO
    {
        [Required(ErrorMessage ="First name is required!")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        public string Username {  get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email  is required!")]
        public string Email { get; set; }

        public string PhoneNumber {  get; set; }
    }
}

// Anotacija Required -> da li objekat sadrzi FirstName