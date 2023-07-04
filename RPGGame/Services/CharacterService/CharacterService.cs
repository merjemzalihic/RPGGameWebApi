
using AutoMapper;
using RPGGame.DTOs;
using RPGGame.DTOs.Character;
using RPGGame.Models;
using RPGGame.Repositories.BaseReppository;
using RPGGame.Repositories.CharacterRepository;
using RPGGame.Services.UserService;
using System.Security.Claims;
using System.Xml;

namespace RPGGame.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private IMapper _mapper;
        private readonly IRepositoryBase<Skill> _repositorySkill;
        private readonly ICharacterRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public CharacterService(IMapper mapper, ICharacterRepository repository, IHttpContextAccessor httpContextAccessor, IUserService userService, IRepositoryBase<Skill> repositorySkill)
        {
            _mapper = mapper;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _repositorySkill= repositorySkill;
        }

        public GetCharacterDto Create(NewCharacterDto dto)
        {
            Character newCharacter = _mapper.Map<Character>(dto);
            int userId = _userService.GetUserIdFromCookie();
            newCharacter.UserId = userId;

            _repository.Create(newCharacter);


            return _mapper.Map<GetCharacterDto>(newCharacter);
        }

        public void Delete(int id)
        {
            Character character = _repository.GetById(id);
            if(character.UserId != _userService.GetUserIdFromCookie())
            {
                throw new Exception("Not found");
            }
            _repository.DeleteById(id);
           
        }

        public List<GetCharacterDto> GetAll()
        {
            try
            {
                int userId = _userService.GetUserIdFromCookie();
                List<Character> characters = _repository.GetAllWithExpression(x => x.UserId == userId);
                
                List<GetCharacterDto> list = _mapper.Map<List<GetCharacterDto>>(characters);

                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public GetCharacterDto GetById(int id)
        {
            int userId = _userService.GetUserIdFromCookie();
            Character? character = _repository.GetSingleWithExpression(x => x.UserId == userId && x.Id == id);


            if (character != null)
            {
                return _mapper.Map<GetCharacterDto>(character);
            } 
                
            throw new Exception("Character not found");
        }

        public GetCharacterDto Update(UpdateCharacterDto character)
        {
            if (character.UserId != _userService.GetUserIdFromCookie())
            {
                throw new Exception("Character not found");
            }

            Character dbCharacter = _mapper.Map<Character>(character);
            _repository.Update(dbCharacter);

            return _mapper.Map<GetCharacterDto>(dbCharacter);
        }

        public GetCharacterDto AddSkill(NewCharacterSkillDto newCharacterSkillDto)
        {
            Character character =  _repository.GetSingleWithExpression(x => x.Id == newCharacterSkillDto.CharacterId);

            int userId = _userService.GetUserIdFromCookie();

            if(character == null) 
            {
                throw new Exception("Character does not exist");
            }

            if (character.UserId != userId) 
            {
                throw new Exception("User does not own Character");
            }

            Skill skill = _repositorySkill.GetById(newCharacterSkillDto.SkillId);

            if (skill == null)
            {
                throw new Exception("Skill does not exist");
            }

            if (character.Skills.Any(x => x.Id == skill.Id))
            {
                throw new Exception("Character alredy has this Skill");
            }
            character.Skills.Add(skill);
            _repository.SaveChanges();


            return _mapper.Map<GetCharacterDto>(character);

        }
    }
}
