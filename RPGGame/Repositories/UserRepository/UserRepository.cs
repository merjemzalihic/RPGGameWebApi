using Microsoft.EntityFrameworkCore;
using RPGGame.Database;
using RPGGame.Models;
using RPGGame.Repositories.BaseReppository;

namespace RPGGame.Repositories.UserRepository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        // Kada naslijedujejemo repository base, uvijek moramo poslati data context preko konstruktora u njega
        public UserRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public override User GetById(int id)
        {
            // Include je isto kao JOIN u SQL. Omogućava nam da izvlačimo podatke iz vise tabela
            var result = Context.Set<User>()
                .Include(x => x.Characters)
                .FirstOrDefault(x => x.Id == id)!;

            return result;
        }
    }
}
