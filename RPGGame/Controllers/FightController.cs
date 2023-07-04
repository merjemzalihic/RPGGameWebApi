using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGGame.DTOs.Character;
using RPGGame.DTOs.Fights;
using RPGGame.Services.CharacterService;
using RPGGame.Services.FightsService;

namespace RPGGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FightController : ControllerBase
    {
        private readonly IFightsService _fightsService;


        public FightController(IFightsService fightsService)
        {
            _fightsService = fightsService;
        }

        [HttpPost("WeaponAttack")]
        public ActionResult<AttackResultDto> WeaponAttack(WeaponAttackDto weaponAttackDto) 
        {
            try
            {
                return Ok(_fightsService.WeaponAttack(weaponAttackDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("SkillAttack")]
        public ActionResult<AttackResultDto> SkillAttack (SkillAttackDto skillAttackDto) 
        {
            try 
            {
                return Ok(_fightsService.SkillAttack(skillAttackDto));
            }
            catch(Exception e) 
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("GetLeadBoard")]
        public ActionResult<List<GetCharacterDto>> GetLeadBoard()
        {
            try 
            {
                return Ok(_fightsService.GetLeadBoard());
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            } 
        }
    }
}
