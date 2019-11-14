using PDV.API.Data.Entities;

namespace PDV.API.Data.Repositories
{
    public class StoreRepository : BaseRepository<Store>
    {
        public StoreRepository(PDVContext context) : base(context)
        {
        }
    }
}
