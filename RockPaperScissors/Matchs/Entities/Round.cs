using RockPaperScissors.Matchs.Enums;

namespace RockPaperScissors.Matchs.Entities
{
    public class Round
    {
        public Choice FirstPlayerChoice { get; private set; } = Choice.Nothing;
        public Choice SecondPlayerChoice { get; private set; } = Choice.Nothing;
        public RoundResult FirstPlayerResult { get; private set; }
        public RoundResult SecondPlayerResult { get; private set; }

        public void SetPlayerChoice(Players player, Choices choice)
        {
            var choiceImplementation = DeterminateChoiceWithEnum(choice);
            
            if (player == Players.First)
            {
                FirstPlayerChoice = choiceImplementation;
                return;
            }

            SecondPlayerChoice = choiceImplementation;
        }

        public void SetRoundResult(RoundResult roundResult)
        {
            FirstPlayerResult = roundResult;
            SecondPlayerResult = 2 - roundResult;
        }

        private Choice DeterminateChoiceWithEnum(Choices choice)
        {
            return choice switch
            {
                Choices.Rock => Choice.Rock,
                Choices.Paper => Choice.Paper,
                Choices.Scissors => Choice.Scissors,
            };
        }
    }
}