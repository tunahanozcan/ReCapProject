using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
   public class EfRntitiyRepositoryBase<TEntitiy,TContext>:IEntityRepository<TEntitiy>
       where TEntitiy : class, IEntity, new()
       where TContext : DbContext, new()
    {
        public void Add(TEntitiy entity)
        {
            using (TContext context = new TContext())
            {
                var addedCar = context.Entry(entity);
                addedCar.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntitiy entity)
        {
            using (TContext context = new TContext())
            {
                var deletedCar = context.Entry(entity);
                deletedCar.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntitiy Get(Expression<Func<TEntitiy, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntitiy>().SingleOrDefault(filter);
            }
        }

        public List<TEntitiy> GetAll(Expression<Func<TEntitiy, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntitiy>().ToList() : context.Set<TEntitiy>().Where(filter).ToList();
            }
        }

        public void Update(TEntitiy entity)
        {
            using (TContext context = new TContext())
            {
                var updatedCar = context.Entry(entity);
                updatedCar.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
