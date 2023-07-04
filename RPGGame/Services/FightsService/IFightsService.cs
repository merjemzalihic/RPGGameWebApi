using RPGGame.DTOs.Character;
using RPGGame.DTOs.Fights;

namespace RPGGame.Services.FightsService
{
    public interface IFightsService
    {
        public AttackResultDto WeaponAttack(WeaponAttackDto weaponAttackDto);
        public AttackResultDto SkillAttack(SkillAttackDto skillAttackDto);
        public List<GetCharacterDto> GetLeadBoard();

    }
}
