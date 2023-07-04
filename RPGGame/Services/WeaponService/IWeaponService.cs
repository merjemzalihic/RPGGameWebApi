using RPGGame.DTOs.Weapon;

namespace RPGGame.Services.WeaponService
{
    public interface IWeaponService
    {
        public GetWeaponDto Create(NewWeaponDto newWeaponDto );
    }
}
