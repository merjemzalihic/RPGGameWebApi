using AutoMapper;
using RPGGame.DTOs.Skill;
using RPGGame.Models;
using RPGGame.Repositories.BaseReppository;
using RPGGame.Services.SkillService;

namespace RPGGame.Services.SkillSerivce
{
    public class SkillService : ISkillService
    {
        private IMapper _mapper;
        private readonly IRepositoryBase<Skill> _repository;

        public SkillService(IMapper mapper, IRepositoryBase<Skill> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public GetSkillDto Create(SkillDto newSkillDto)
        {
            Skill newSkill = _mapper.Map<Skill>(newSkillDto);
            _repository.Create(newSkill);
            return _mapper.Map<GetSkillDto>(newSkill);
        }

        public void Delete(int id)
        {
            _repository.DeleteById(id);
        }

        public List<GetSkillDto> GetAll()
        {
            List<Skill> skills = _repository.GetAll();

            return _mapper.Map<List<GetSkillDto>>(skills);

        }

        public GetSkillDto Update(SkillDto newSkillDto)
        {
            if (newSkillDto.Id == null) 
            {
                throw new Exception("Not found");
            }

            Skill updatedSkill = _mapper.Map<Skill>(newSkillDto);

            _repository.Update(updatedSkill);
            return _mapper.Map<GetSkillDto>(updatedSkill);
        }
    }
}
