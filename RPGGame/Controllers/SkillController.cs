using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGGame.DTOs.Skill;
using RPGGame.Services.SkillService;

namespace RPGGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<GetSkillDto>> GetALl() 
        {
            return Ok(_skillService.GetAll());
        }
        [HttpPost("Create")]
        public ActionResult<GetSkillDto> Create(SkillDto newSkillDto)
        {
            return Ok(_skillService.Create(newSkillDto));
        }
        [HttpPut("Update")]
        public ActionResult<List<GetSkillDto>> Update(SkillDto newSkillDto) 
        {
            return Ok(_skillService.Update(newSkillDto));
        }
        [HttpDelete("Delete/{id}")]
        public ActionResult Delete (int id) 
        {
        try
            {
                _skillService.Delete(id);
                return Ok();    

            }
            catch (Exception e) 
            {
            return BadRequest(e.Message);
            }
        }
    }
}
