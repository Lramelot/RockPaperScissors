using System;
using System.Collections.Generic;
using System.Linq;
using FluentResults;
using RockPaperScissors.Matchs.Entities;
using RockPaperScissors.Matchs.Enums;

namespace RockPaperScissors.Matchs
{
    public class Match
    {
        private const int MaxRound = 5;
        
        public bool IsFinished;
        public string FirstPlayerName => _firstPlayer.Name;
        public string SecondPlayerName => _secondPlayer.Name;
        public string WinnerName => ComputeWinner().Name;
        public int RoundNumber => _rounds.Count;
        
        private Player _firstPlayer { get; }
        private Player _secondPlayer { get; }
        private IList<Round> _rounds { get; } = new List<Round>();

        private Round CurrentRound => _rounds.Last();
        
        private Match(Player firstPlayer, Player secondPlayer)
        {
            _firstPlayer = firstPlayer;
            _secondPlayer = secondPlayer;
            
            CreateRound();
        }

        public static Result<Match> StartMatch(string firstPlayerName, string secondPlayerName)
        {
            if (firstPlayerName == secondPlayerName)
            {
                return Result.Fail("Name should be differents");
            }

            var firstPlayerResult = Player.Create(firstPlayerName);
            if (firstPlayerResult.IsFailed)
            {
                var creationResult = Result.Fail("First player creation failed");
                return Result.Merge(creationResult, firstPlayerResult).ToResult();
            }
            
            var secondPlayerResult = Player.Create(secondPlayerName);
            if (secondPlayerResult.IsFailed)
            {
                var creationResult = Result.Fail("Second player creation failed");
                return Result.Merge(creationResult, secondPlayerResult).ToResult();
            }

            return Result.Ok(new Match(firstPlayerResult.Value, secondPlayerResult.Value));
        }

        public void SetFirstPlayerChoice(Choices choice)
        {
            if (IsFinished)
            {
                throw new ApplicationException("The game is over, you can't make a new play!");
            }

            CurrentRound.SetPlayerChoice(Players.First, choice);
        }
        
        public void SetSecondPlayerChoice(Choices choice)
        {
            if (IsFinished)
            {
                throw new ApplicationException("The game is over, you can't make a new play!");
            }

            CurrentRound.SetPlayerChoice(Players.Second, choice);
        }

        public void ComputeRound()
        {
            var roundResult = CurrentRound.FirstPlayerChoice.CompeteWith(CurrentRound.SecondPlayerChoice);
            CurrentRound.SetRoundResult(roundResult);

            if (RoundNumber >= MaxRound)
            {
                IsFinished = true;
                return;
            }
            
            CreateRound();
        }

        private void CreateRound()
        {
            _rounds.Add(new Round());
        }
        
        private Player ComputeWinner()
        {
            if (!IsFinished)
            {
                throw new ApplicationException("The game is not over, you can't get the winner!");
            }

            var firstPlayerScore = _rounds.Sum(r => (int) r.FirstPlayerResult);
            var secondPlayerScore = _rounds.Sum(r => (int) r.SecondPlayerResult);

            if (firstPlayerScore > secondPlayerScore)
            {
                return _firstPlayer;
            }

            if (firstPlayerScore == secondPlayerScore)
            {
                return Player.TiePlayer;
            }

            return _secondPlayer;
        } 
    }
}