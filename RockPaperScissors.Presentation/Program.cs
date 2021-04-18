using System;
using System.Linq;
using RockPaperScissors.Matchs;
using RockPaperScissors.Matchs.Enums;

namespace RockPaperScissors.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstPlayerName = AskForPlayerName("Nom du premier joueur: ");
            var secondPlayerName = AskForPlayerName("Nom du second joueur: ");

            var matchResult =  Match.StartMatch(firstPlayerName, secondPlayerName);
            if (matchResult.IsFailed)
            {
                Console.WriteLine(matchResult.Errors.Select(e => e.Message + Environment.NewLine));
            }

            var match = matchResult.Value;
            while (!match.IsFinished)
            {
                Console.WriteLine($"=== Round numéro {match.RoundNumber} ===");
                Console.WriteLine("1 Pierre | 2 Paper | 3 Ciseaux");
                
                var firstPlayerChoice = AskForPlayerChoice(match.FirstPlayerName);
                match.SetFirstPlayerChoice(firstPlayerChoice);
                
                var secondPlayerChoice = AskForPlayerChoice(match.SecondPlayerName);
                match.SetSecondPlayerChoice(secondPlayerChoice);
                
                match.ComputeRound();
            }

            Console.WriteLine($"Le grand gagnant est: {match.WinnerName}");
            Console.WriteLine("Merci d'avoir joué !");
        }

        private static string AskForPlayerName(string message)
        {
            Console.Write(message);
            var playerName = Console.ReadLine();
            return playerName;
        }

        private static Choices AskForPlayerChoice(string playerName)
        {
            Console.Write($"{playerName}, faites votre choix.");
            // Aucune gestion d'erreur volontaire sur le choix de l'action
            var choice = int.Parse(Console.ReadLine() ?? "1");

            return (Choices) choice;
        }
    }
}