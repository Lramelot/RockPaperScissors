using System;

namespace RockPaperCisor.Domain.Domain
{
    public class Rock : Hand
    {
        protected override Type LosingHand => typeof(Paper);
    }
}
