using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDV.API.Data.Entities
{
    public class AccountStore
    {
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
    }
}
