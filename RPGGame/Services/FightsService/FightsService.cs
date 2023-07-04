using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPGGame.Database;
using RPGGame.DTOs.Character;
using RPGGame.DTOs.Fights;
using RPGGame.Models;
using RPGGame.Repositories.BaseReppository;
using RPGGame.Repositories.CharacterRepository;

namespace RPGGame.Services.FightsService
{
    public class FightsService : IFightsService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IDataContext _context;
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<Skill> _repositorySkill;
        private readonly ICharacterRepository _repositoryCharacter;


        public FightsService(ICharacterRepository characterRepository, IDataContext context, IMapper mapper, IRepositoryBase<Skill> repositorySkill,
            ICharacterRepository repositoryCharacter)
        {
            _characterRepository = characterRepository;
            _context = context;
            _mapper = mapper;
            _repositorySkill = repositorySkill;
            _repositoryCharacter = repositoryCharacter;
        }

        public AttackResultDto WeaponAttack(WeaponAttackDto weaponAttackDto)
        {
            Character attacker = _context.Set<Character>()
                .Include(x => x.Weapon)
                .FirstOrDefault(x => x.Id == weaponAttackDto.AttackerId)!;

            Character opponent = _context.Set<Character>()
                .FirstOrDefault(x => x.Id == weaponAttackDto.OpponentId)!;

            if (attacker == null || opponent == null)
            {
                throw new Exception("Bad Request");
            }

            if (attacker.Weapon == null)
            {
                throw new Exception("Attacker has no weapon");
            }

            double damage = (attacker.Strength + attacker.Weapon.Damage) - opponent.Defense;
            opponent.HitPoints = opponent.HitPoints - Convert.ToInt32(damage);

            _context.SaveChanges();

            string result = GetFightResult(opponent);

            return new AttackResultDto()
            {
                Attacker = _mapper.Map<GetCharacterDto>(attacker),
                Opponent = _mapper.Map<GetCharacterDto>(opponent),
                Damage = damage,
                Result = result
            };

        }



        public AttackResultDto SkillAttack(SkillAttackDto skillAttackDto)
        {
            Skill skill = _repositorySkill.GetById(skillAttackDto.SkillId);
            Character attacker = _repositoryCharacter.GetSingleWithExpression(x => x.Id == skillAttackDto.AttackerID);
            Character opponent = _repositoryCharacter.GetSingleWithExpression(x => x.Id == skillAttackDto.OpponentId);

            if (attacker == null || opponent == null || skill == null)
            {
                throw new Exception("Bad Request");
            }

            if (attacker.Skills != null && !attacker.Skills.Any(x => x.Id == skill.Id))
            {
                throw new Exception("Attacker does not have this Skill");
            }
            double damage = (attacker.Intelligence + skill.Damage) - opponent.Defense;
            opponent.HitPoints = opponent.HitPoints - Convert.ToInt32(damage);

            _repositoryCharacter.SaveChanges();

            return new AttackResultDto()
            {
                Attacker = _mapper.Map<GetCharacterDto>(attacker),
                Opponent = _mapper.Map<GetCharacterDto>(opponent),
                Damage = damage,
                Result = GetFightResult(opponent)
            };
        }
        public List<GetCharacterDto> GetLeadBoard()
        {
            List<Character> characters = _repositoryCharacter.GetAll().OrderBy(x => x.Defense).OrderByDescending(x=> x.Victories).ToList();

            return _mapper.Map<List<GetCharacterDto>>(characters);
        }

        private static string GetFightResult(Character opponent)
        {
            string result = string.Empty;
            if (opponent.HitPoints <= 0)
            {
                result = $"{opponent.Name} is dead";
            }
            else
            {
                result = $"{opponent.Name} has {opponent.HitPoints} HP left";
            }

            return result;
        }
        
    }
}
