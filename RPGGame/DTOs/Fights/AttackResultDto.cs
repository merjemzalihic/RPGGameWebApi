using RPGGame.DTOs.Character;

namespace RPGGame.DTOs.Fights
{
    public class AttackResultDto
    {
        public GetCharacterDto Attacker { get; set; }
        public GetCharacterDto Opponent { get; set; }
        public double Damage { get; set; }
        public string Result { get; set; }
    }
}

