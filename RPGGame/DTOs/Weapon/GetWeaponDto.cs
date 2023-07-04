using RPGGame.DTOs.Character;

namespace RPGGame.DTOs.Weapon
{
    public class GetWeaponDto : BaseModelDto
    {
        public string Name { get; set; }
        public double Damage { get; set; }
        public int CharacterId { get; set; }
    }
}
