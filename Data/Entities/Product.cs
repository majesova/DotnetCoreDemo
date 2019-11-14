using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDV.API.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public Store Store { get; set; }
        public Guid StoreId { get; set; }
    }
}
