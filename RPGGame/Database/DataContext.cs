using Microsoft.EntityFrameworkCore;
using RPGGame.Models;

namespace RPGGame.Database
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public void Seed()
        {
            if(!Skills.Any())
            {
                Skill skill = new Skill()
                {
                    Name = "Fireball",
                    Damage = 70
                };

                Skills.Add(skill);

                Skills.Add(new Skill()
                {
                    Name = "Thunder",
                    Damage = 100
                });
            }


            SaveChanges();
        }       
    }
}
