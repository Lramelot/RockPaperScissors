using System;

namespace RockPaperCisor.Domain.Domain
{
    public class Scissor : Hand
    {
        protected override Type LosingHand => typeof(Rock);
    }
}
