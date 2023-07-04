using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGGame.DTOs;
using RPGGame.DTOs.Character;
using RPGGame.Models;
using RPGGame.Services.CharacterService;

namespace RPGGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CharacterController : ControllerBase
    {
        private ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<GetCharacterDto>> GetAll()
        {
            return Ok(_characterService.GetAll());
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<GetCharacterDto> GetById(int id)
        {
            try
            {
                return Ok(_characterService.GetById(id));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("Create")]
        public ActionResult<List<GetCharacterDto>> Create(NewCharacterDto character)
        {
            return Ok(_characterService.Create(character));
        }

        [HttpPut("Update")]
        public ActionResult<List<GetCharacterDto>> Update(UpdateCharacterDto character)
        {
            return Ok(_characterService.Update(character));
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _characterService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("AddSkill")]
        public ActionResult<GetCharacterDto> AddSkill(NewCharacterSkillDto newCharacterSkillDto)
        {
            try
            {
                return Ok(_characterService.AddSkill(newCharacterSkillDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
