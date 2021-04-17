using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperCisor.Domain.Domain
{
    public class Player
    {
        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;
        }

        public bool IsIdentical(Player player)
        {
            return Name == player?.Name;
        }
    }
}
