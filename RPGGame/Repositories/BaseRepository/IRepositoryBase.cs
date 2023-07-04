using RPGGame.Models;
using System.Linq.Expressions;

namespace RPGGame.Repositories.BaseReppository
{
    public interface IRepositoryBase<T> where T : BaseModel
    {
        public List<T> GetAll();
        public T GetById(int id);
        public T Create(T entity);
        public T Update(T entity);
        public void DeleteById(int id);
        public T GetSingleWithExpression(Expression<Func<T, bool>> expression);
        public List<T> GetAllWithExpression(Expression<Func<T, bool>> expression);
        public void SaveChanges();
    }
}
