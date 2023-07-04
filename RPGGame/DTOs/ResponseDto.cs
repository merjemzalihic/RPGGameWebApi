using RPGGame.DTOs.Character;

namespace RPGGame.DTOs
{
    public class ResponseDto<T>
    {
        public bool IsSuccessful { get; set; } = true;
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }
    }
}
