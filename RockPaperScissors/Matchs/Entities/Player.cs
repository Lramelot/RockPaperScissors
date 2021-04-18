using FluentResults;

namespace RockPaperScissors.Matchs.Entities
{
    public class Player
    {
        public static Player TiePlayer = new Player("Tie"); 
        
        public string Name { get; }

        private Player(string name)
        {
            Name = name;
        }

        public static Result<Player> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result.Fail("Name should not empty");
            }

            return Result.Ok(new Player(name));
        }
    }
}