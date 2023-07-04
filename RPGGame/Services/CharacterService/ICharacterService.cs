using RPGGame.DTOs;
using RPGGame.DTOs.Character;
using RPGGame.Models;

namespace RPGGame.Services.CharacterService
{
    public interface ICharacterService
    {
        public List<GetCharacterDto> GetAll();
        public GetCharacterDto GetById(int id);
        public GetCharacterDto Create(NewCharacterDto character);
        public GetCharacterDto Update(UpdateCharacterDto character);
        public void   Delete(int id);
        public GetCharacterDto AddSkill(NewCharacterSkillDto newCharacterSkillDto);
    }
}
