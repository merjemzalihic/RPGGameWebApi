using System.ComponentModel.DataAnnotations.Schema;

namespace RPGGame.Models
{
    public class Weapon : BaseModel
    {
        public string Name { get; set; }  
        public double Damage { get; set; }
        

        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        public Character Character { get; set; }

    }
}
