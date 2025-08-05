using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    //generic metotlarla işlemleri yazmam lazım!
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity: class, new()
        where TContext : DbContext, new()
    {


        //ekleme işlemi için yapılan generic yapı!
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                //entity i eşleştiriyoruz!
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                //entity i eşleştiriyoruz!
                addedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            //bu kod bloğu e(x => x.Id == id) 
            //filtrelemeyi sağlıyor!
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        //bu kod bloğu e(x => x.Id == id) 
        //filtrelemeyi sağlıyor!
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                //entity i eşleştiriyoruz!
                addedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
