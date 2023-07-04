using AutoMapper;
using RPGGame.DTOs.Character;
using RPGGame.DTOs.Skill;
using RPGGame.DTOs.User;
using RPGGame.DTOs.Weapon;
using RPGGame.Models;

namespace RPGGame.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Character, GetCharacterDto>().ReverseMap();
            CreateMap<Character, UpdateCharacterDto>().ReverseMap();
            CreateMap<Character, NewCharacterDto>().ReverseMap();
            CreateMap<User, RegistrationDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Weapon, GetWeaponDto>().ReverseMap();
            CreateMap<Weapon, NewWeaponDto>().ReverseMap();
            CreateMap<Skill, GetSkillDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();

        }
    }
}
