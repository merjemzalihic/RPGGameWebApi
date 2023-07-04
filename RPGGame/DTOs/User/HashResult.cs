namespace RPGGame.DTOs.User
{
    public class HashResult
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
