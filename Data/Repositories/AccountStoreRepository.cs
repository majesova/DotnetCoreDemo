using PDV.API.Data.Repositories;
using PDV.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDV.API.Data.Repositories
{
    public class AccountStoreRepository : BaseRepository<AccountStore>
    {
        public AccountStoreRepository(PDVContext context) : base(context)
        {
        }

        public List<Store> GetStoresByAccountId(string id)
        {
            return _context.AccountStores.Where(x => x.AccountId == id)
                .Select(x=>x.Store).ToList();
        }
    }
}
