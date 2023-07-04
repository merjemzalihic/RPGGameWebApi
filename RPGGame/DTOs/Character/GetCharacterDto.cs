using RPGGame.DTOs.Skill;
using RPGGame.DTOs.Weapon;
using RPGGame.Enums;

namespace RPGGame.DTOs.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "Frodo";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;

        public int Defense { get; set; } = 10;

        public int Intelligence { get; set; } = 10;

        public CharacterClassEnum Class { get; set; } = CharacterClassEnum.Knight;

        public int UserId { get; set; }

        public GetWeaponDto? Weapon { get; set; }

        public List<GetSkillDto>? Skills { get; set; }
    }
}
