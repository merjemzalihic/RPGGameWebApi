using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RPGGame.DTOs;
using RPGGame.DTOs.User;
using RPGGame.Models;
using RPGGame.Repositories.BaseReppository;
using RPGGame.Repositories.UserRepository;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace RPGGame.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public UserService(IMapper mapper, IUserRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _repository = repository;
            HttpContextAccessor = httpContextAccessor;
        }

        public UserDto Register(RegistrationDto registrationDto)
        {
            User user = _mapper.Map<User>(registrationDto);

            HashResult hashResult = HashPassword(registrationDto.Password);

            user.PasswordSalt = hashResult.PasswordSalt;
            user.PasswordHash = hashResult.PasswordHash;

            _repository.Create(user);

            return _mapper.Map<UserDto>(user);
        }

        public List<UserDto> GetAll()
        {
            List<User> users = _repository.GetAll();

            return _mapper.Map<List<UserDto>>(users);
        }


        public void Delete(int id)
        {
            _repository.DeleteById(id);
        }

        public UserDto Update(UserDto userDto)
        {
            User dbUser = _repository.GetById(userDto.Id);
            User user = _mapper.Map(userDto, dbUser);

            _repository.Update(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<ResponseDto<UserDto>> LogIn(LoginRequestDto loginRequestDto)
        {
            User user = _repository.GetSingleWithExpression(x => x.Username == loginRequestDto.Username);

            if (user == null)
            {
                ResponseDto<UserDto> responseDto = new ResponseDto<UserDto>()
                {
                    IsSuccessful = false,
                    ErrorMessage = "This account was not found"
                };

                return responseDto;
            }

            if (!IsPasswordValid(loginRequestDto.Password, user))
            {
                return new ResponseDto<UserDto> { IsSuccessful = false, ErrorMessage = "Password is not correct" };
            }

            await CreateCookie(user);

            return new ResponseDto<UserDto>
            {
                IsSuccessful = true,
                Data = _mapper.Map<UserDto>(user)
            };
        }

        private HashResult HashPassword(string password)
        {
            HashResult result = new HashResult();

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                result.PasswordSalt = hmac.Key;
                result.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return result;
        }

        private bool IsPasswordValid(string password, User user)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
            {
                var result = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return result.SequenceEqual(user.PasswordHash);
            }
        }

        private async Task CreateCookie(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            await HttpContextAccessor.HttpContext.SignInAsync(
                         CookieAuthenticationDefaults.AuthenticationScheme,
                         new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));
        }

        public int GetUserIdFromCookie()
        {
            return Convert.ToInt32(HttpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }

        public UserDto GetMyData()
        {
            int userId = GetUserIdFromCookie();
            User user = _repository.GetById(userId);
            return _mapper.Map<UserDto>(user);
        }
    }
}
