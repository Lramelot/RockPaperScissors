using System;
using System.Collections.Generic;
using System.Linq;

using FluentResults;

using RockPaperCisor.Domain.Domain.Enums;

namespace RockPaperCisor.Domain.Domain
{
    public class Match
    {
        public Guid Id { get; set; }

        public Player Player1 { get; private set; }

        public Player Player2 { get; }

        public GameState State { get; private set; }

        public IReadOnlyList<Round> Rounds => _rounds.ToList().AsReadOnly();

        private readonly ICollection<Round> _rounds = new List<Round>();

        private Match(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            State = GameState.Starting;
        }

        public static Result<Match> Create(Player player1, Player player2)
        {
            if (player1 == null || player2 == null)
            {
                return Result.Fail("Player should exist");
            }

            if (player1.IsIdentical(player2))
            {
                return Result.Fail("Player should be different");
            }

            var match = new Match(player1, player2);
            return Result.Ok(match);
        }

        public Result StartNewRound()
        {
            if (MatchIsFinished)
            {
                return Result.Fail("Match is finished");
            }

            var newRound = Round.CreateRound();
            _rounds.Add(newRound);

            return Result.Ok();
        }

        public Result<Player> GetWinner()
        {
            if (TotalOfRoundsPlayed < MustWinNumber)
            {
                return Result.Fail("Game is not ended");
            }

            if (Player1WonRounds >= MustWinNumber)
            {
                State = GameState.Done;
                return Result.Ok(Player1);
            }

            if (Player2WonRounds >= MustWinNumber)
            {
                State = GameState.Done;
                return Result.Ok(Player2);
            }

            if (IsTie)
            {
                State = GameState.Done;
                return Result.Ok();
            }

            return Result.Fail("Game is not ended");
        }

        public Result SetPlayerVote(Player player, Hand vote)
        {
            var roundResult = GetPlayingRound();

            if (roundResult.IsFailed)
            {
                return Result.Fail("Round not found");
            }

            var round = roundResult.Value;
            if (IsPlayer1(player))
            {
                round.SetPlayer1Vote(vote);
                return Result.Ok();
            }

            if (IsPlayer2(player))
            {
                round.SetPlayer2Vote(vote);
                return Result.Ok();
            }

            return Result.Fail("Player is not in the game");
        }

        public Result EndPlayingRound()
        {
            var roundResult = GetPlayingRound();

            if (roundResult.IsFailed)
            {
                return Result.Fail("Round not found");
            }

            var round = roundResult.Value;
            round.Stop();
            return Result.Ok();
        }

        private const uint MaxRound = 5;
        private const uint MustWinNumber = MaxRound / 2 + 1;

        private int Player1WonRounds => _rounds.Where(r => r.Winner == Winner.Player1).Count();
        private int Player2WonRounds => _rounds.Where(r => r.Winner == Winner.Player2).Count();
        private int TotalOfRoundsPlayed => _rounds.Count();
        private bool IsTie => Player1WonRounds == Player2WonRounds && TotalOfRoundsPlayed >= MaxRound;
        private bool MatchIsFinished => State == GameState.Done || TotalOfRoundsPlayed >= MaxRound;
        private bool IsPlayer1(Player player) => Player1.Name == player.Name;
        private bool IsPlayer2(Player player) => Player2.Name == player.Name;

        private Result<Round> GetPlayingRound()
        {
            var round = _rounds.SingleOrDefault(r => r.State == RoundState.WaitingForAnswer);

            if (round == null)
            {
                return Result.Fail("No playing round");
            }

            return Result.Ok(round);
        }
    }
}
