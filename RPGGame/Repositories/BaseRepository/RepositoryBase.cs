using RPGGame.Database;
using RPGGame.Models;
using System.Linq.Expressions;

namespace RPGGame.Repositories.BaseReppository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseModel
    {
        public readonly IDataContext Context;

        public RepositoryBase(IDataContext dataContext)
        {
            Context = dataContext;
        }

        public virtual List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            return Context.Set<T>().FirstOrDefault(x => x.Id == id)!;
        }

        public virtual T Create(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public virtual void DeleteById(int id)
        {
            T entity = GetById(id);

            if (entity == null)
            {
                throw new Exception("Entity not found");
            } 

            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public virtual T Update(T entity)
        {
            Context.Set<T>().Update(entity);
            Context.SaveChanges();
            return entity;
        }

        public virtual T GetSingleWithExpression(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression).FirstOrDefault();
        }

        public virtual List<T> GetAllWithExpression(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression).ToList();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
