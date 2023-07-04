using AutoMapper;
using RPGGame.DTOs.Weapon;
using RPGGame.Models;
using RPGGame.Repositories.WeaponRepository;

namespace RPGGame.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper _mapper;
       private readonly IWeaponRepository _repository;

        public WeaponService(IMapper mapper, IWeaponRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public GetWeaponDto Create(NewWeaponDto newWeaponDto)
        {
            Weapon weapon = _mapper.Map<Weapon>(newWeaponDto);
            Weapon  newWeapon = _repository.Create(weapon);
            return _mapper.Map<GetWeaponDto>(newWeapon);
        }
    }
}
