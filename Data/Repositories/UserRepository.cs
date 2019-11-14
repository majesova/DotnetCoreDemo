using PDV.API.Data;
using PDV.API.Data.Entities;

namespace PDV.API.Data.Repositories
{
    public class UserRepository : BaseRepository<Account>
    {
        public UserRepository(PDVContext context):base(context)
        {
        }   
    }
}