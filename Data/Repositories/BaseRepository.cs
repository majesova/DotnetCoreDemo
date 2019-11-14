using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PDV.API.Data.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly PDVContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(PDVContext context)
        {
            _context = context; 
            _dbSet = _context.Set<TEntity>();   
        }

        public List<TEntity> GetAll(){
            return _dbSet.ToList();
        }

        public void Insert(TEntity entity){
            _dbSet.Add(entity);
        }

        public TEntity GetById(params object[] keyValues){
            return _dbSet.Find(keyValues);
        }

          public void Update(TEntity entity){
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(params object[] keyValues){
            var entityForDelete = _dbSet.Find(keyValues);
            _dbSet.Remove(entityForDelete);     
        }




    }

}