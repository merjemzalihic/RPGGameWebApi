using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGGame.DTOs;
using RPGGame.DTOs.User;
using RPGGame.Services.UserService;

namespace RPGGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public ActionResult<UserDto> Register(RegistrationDto registrationDto)
        {
            try
            {
                return Ok(_userService.Register(registrationDto));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("GetAll")]
        public ActionResult<List<UserDto>> GetAll() 
        {
            try
            {
                return _userService.GetAll();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("GetMyData")]
        public ActionResult<UserDto> GetMyData() 
        {
            return _userService.GetMyData();
        }

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id) 
        {
            try
            {
                _userService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("Update")]
        public ActionResult<UserDto> Update([FromBody] UserDto userDto) 
        {
            try
            {
                UserDto result = _userService.Update(userDto);
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
        }

        [HttpPost("LogIn")]
        public ActionResult<ResponseDto<UserDto>> LogIn (LoginRequestDto loginRequestDto) 
        {
            try
            {
                return Ok(_userService.LogIn(loginRequestDto));   
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}
