namespace RPGGame.Models
{
    public class Skill : BaseModel
    { 
        public string Name { get; set; }
        public double Damage { get; set; }
        public List<Character>? Characters { get; set; }
    }
}
