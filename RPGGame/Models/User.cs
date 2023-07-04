namespace RPGGame.Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set;  }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public List<Character>? Characters { get; set; }

        public string GetFullName() 
        {
            return FirstName + " " + LastName;
        }
    }
}
