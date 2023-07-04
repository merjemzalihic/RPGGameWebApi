using RPGGame.DTOs.Skill;

namespace RPGGame.Services.SkillService
{
    public interface ISkillService
    {
        public List<GetSkillDto> GetAll();
        public GetSkillDto Create(SkillDto newSkillDto);
        public GetSkillDto Update(SkillDto newSkillDto);
        public void Delete(int id);
    }
}
