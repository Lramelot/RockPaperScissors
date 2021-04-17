using System;

using RockPaperCisor.Domain.Domain.Enums;

namespace RockPaperCisor.Domain.Domain
{
    public class Round
    {
        public Guid Id { get; private set; }

        public RoundState State { get; set; }

        public Hand Player1Vote { get; private set; } = default;

        public Hand Player2Vote { get; private set; } = default;

        public Winner Winner => GetWinner();

        private Round()
        {
            Id = Guid.NewGuid();
            State = RoundState.WaitingForAnswer;
        }

        public static Round CreateRound()
        {
            var round = new Round();
            return round;
        }

        public void SetPlayer1Vote(Hand vote) => Player1Vote = vote;

        public void SetPlayer2Vote(Hand vote) => Player2Vote = vote;

        private Winner GetWinner()
        {
            var winningVote = Rules.GetWinningVote(Player1Vote, Player2Vote);

            if (winningVote == default)
            {
                return Winner.None;
            }
            else if (winningVote == Player1Vote)
            {
                return Winner.Player1;
            }
            return Winner.Player2;
        }

        public void Stop() => State = RoundState.Done;

        public bool AllPlayersVoted => Player1HasVoted && Player2HasVoted;

        private bool Player1HasVoted => Player1Vote != default;

        private bool Player2HasVoted => Player2Vote != default;
    }
}
