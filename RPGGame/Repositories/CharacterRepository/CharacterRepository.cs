using Microsoft.EntityFrameworkCore;
using RPGGame.Database;
using RPGGame.Models;
using RPGGame.Repositories.BaseReppository;
using System.Linq;
using System.Linq.Expressions;

namespace RPGGame.Repositories.CharacterRepository
{
    public class CharacterRepository : RepositoryBase<Character>, ICharacterRepository
    {
        public CharacterRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public override List<Character> GetAllWithExpression(Expression<Func<Character, bool>> expression)
        {
            return Context.Set<Character>()
                .Include(x => x.Weapon)
                .Include(x => x.Skills)
                .Where(expression)
                .ToList();
        }

        public override Character GetSingleWithExpression(Expression<Func<Character, bool>> expression)
        {
            return Context.Set<Character>()
                .Include(x => x.Weapon)
                .Include(x => x.Skills)
                .FirstOrDefault(expression);
        }
    }
}
