using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PDV.API.Controllers.Dto
{
    public class RegisterStoreRequestDto
    {
        [Required]
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; } //Puede ser el mismo que el email 
        [Required]
        public string Password { get; set; }
    }
}
