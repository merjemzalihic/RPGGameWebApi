using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGGame.DTOs.Weapon;
using RPGGame.Services.WeaponService;

namespace RPGGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpPost("Create")]
        public ActionResult<GetWeaponDto> Create(NewWeaponDto newWeaponDto)
        {
            try
            {
                return Ok(_weaponService.Create(newWeaponDto));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
    
}
