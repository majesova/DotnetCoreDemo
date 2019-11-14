using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDV.API.Controllers.Dto
{
    public class RegisterProductRequestDto
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
    }
    public class RegisterProductResponseDto
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
    }
}
