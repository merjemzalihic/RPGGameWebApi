using RPGGame.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGGame.Models
{
    public class Character : BaseModel
    {
        public string Name { get; set; } = "Frodo";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;

        public int Defense { get; set; } = 10;

        public int Intelligence { get; set; } = 10;

        public int Fights { get; set; } = 0;
        public int Victories { get; set; } = 0;
        public int Defeats { get; set; } = 0;

        public CharacterClassEnum Class { get; set; } = CharacterClassEnum.Knight;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public Weapon? Weapon { get; set; }
        public List<Skill>? Skills { get; set; }
    }
}
