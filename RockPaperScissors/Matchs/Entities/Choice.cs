using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RockPaperScissors.Matchs.Enums;

namespace RockPaperScissors.Matchs.Entities
{
    public abstract class Choice
    {
        public static readonly Choice Rock = new Rock();
        public static readonly Choice Paper = new Paper();
        public static readonly Choice Scissors = new Scissors();
        public static readonly Choice Nothing = new Nothing();

        protected abstract IEnumerable<Choice> WinAgainst { get; }
        protected abstract IEnumerable<Choice> TieWith { get; }
        
        public RoundResult CompeteWith(Choice choice)
        {
            var isWinning = WinAgainst.Contains(choice);
            if (isWinning)
            {
                return RoundResult.Win;
            }

            var isTie = TieWith.Contains(choice);
            if (isTie)
            {
                return RoundResult.Tie;
            }

            return RoundResult.Lose;
        }
    }
    
    public class Rock : Choice
    {
        protected override IEnumerable<Choice> WinAgainst => new List<Choice> { Scissors, Nothing };
        protected override IEnumerable<Choice> TieWith => new List<Choice> { Rock };
    }
    
    public class Paper : Choice
    {
        protected override IEnumerable<Choice> WinAgainst => new List<Choice> { Rock, Nothing };
        protected override IEnumerable<Choice> TieWith => new List<Choice> { Paper };
    }
    
    public class Scissors : Choice
    {
        protected override IEnumerable<Choice> WinAgainst => new List<Choice> { Paper, Nothing };
        protected override IEnumerable<Choice> TieWith => new List<Choice> { Scissors };
    }
    
    public class Nothing : Choice
    {
        protected override IEnumerable<Choice> WinAgainst => new List<Choice>();
        protected override IEnumerable<Choice> TieWith => new List<Choice> { Nothing };
    }
}