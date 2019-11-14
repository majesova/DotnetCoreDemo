using PDV.API.Data.Entities;

namespace PDV.API.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(PDVContext context) : base(context)
        {
        }
    }
}
