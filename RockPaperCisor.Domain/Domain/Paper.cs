using System;

namespace RockPaperCisor.Domain.Domain
{
    public class Paper : Hand
    {
        protected override Type LosingHand => typeof(Scissor);
    }
}
