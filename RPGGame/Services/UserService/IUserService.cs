using RPGGame.DTOs;
using RPGGame.DTOs.User;

namespace RPGGame.Services.UserService
{
    public interface IUserService
    {
        public UserDto Register(RegistrationDto registrationDto);
        public List<UserDto> GetAll();
        public void Delete(int id);
        public UserDto Update(UserDto userDto);
        public Task<ResponseDto<UserDto>> LogIn(LoginRequestDto loginRequestDto);
        public int GetUserIdFromCookie();
        public UserDto GetMyData();
    }
}
