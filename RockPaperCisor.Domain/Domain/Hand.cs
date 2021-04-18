using System;

namespace RockPaperCisor.Domain.Domain
{
    public abstract class Hand
    {
        private const int LosingValue = 2;
        private const int WinningValue = 1;
        private const int TieValue = 0;

        protected abstract Type LosingHand { get; }

        public int IsWinningAgainst(Hand opponent)
        {
            var opponentType = opponent.GetType();

            if (opponentType == LosingHand)
            {
                return LosingValue;
            }

            if (opponentType == GetType())
            {
                return TieValue;
            }

            return WinningValue;
        }
    }
}
