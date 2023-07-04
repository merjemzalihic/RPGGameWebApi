using Microsoft.EntityFrameworkCore;
using RPGGame.Database;
using RPGGame.Models;
using RPGGame.Repositories.BaseReppository;

namespace RPGGame.Repositories.WeaponRepository
{
    public class WeaponRepository : RepositoryBase<Weapon>, IWeaponRepository
    {
        public WeaponRepository(IDataContext dataContext) : base(dataContext)
        {
        }
        public override Weapon Create(Weapon entity)
        {
            Context.Set<Weapon>().Add(entity);
            Context.SaveChanges();

            Weapon weapon = Context.Set<Weapon>()
                .Include(x=> x.Character)
                .FirstOrDefault(x => x.Id == entity.Id);

            return weapon;
        }
    }
}
