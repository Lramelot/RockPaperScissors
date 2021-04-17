using RockPaperCisor.Domain.Domain.Enums;

namespace RockPaperCisor.Domain.Domain
{
    public static class Rules
    {
        public static Hand GetWinningVote(Hand a, Hand b)
        {
            if (VotesAreIdentical(a, b))
            {
                return default;
            }

            var result = a.IsWinningAgainst(b);
            if (Player1Won(result))
            {
                return a;
            }
            else if (Player2Won(result))
            {
                return b;
            }

            return default;
        }

        public static bool VotesAreIdentical(Hand a, Hand b) => a.GetType() == b.GetType();

        private static bool Player1Won(int winner) => winner == (int)Winner.Player1;
        private static bool Player2Won(int winner) => winner == (int)Winner.Player2;
    }
}
