using System.ComponentModel.DataAnnotations;

namespace PDV.API.Controllers.Dtos
{
    /// <summary>
    /// Dto for register new users
    /// </summary>
    public class RegisterUserRequestDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
    /// <summary>
    /// Dto for response a creation request
    /// </summary>
     public class RegisterUserResponseDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
    /// <summary>
    /// Dto for login
    /// </summary>
    public class LoginDto{
         [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }

}