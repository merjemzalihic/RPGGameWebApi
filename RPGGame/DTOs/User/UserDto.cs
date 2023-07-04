using RPGGame.DTOs.Character;

namespace RPGGame.DTOs.User
{
    public class UserDto : BaseModelDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public List<GetCharacterDto> Characters { get; set; }
    }
}
