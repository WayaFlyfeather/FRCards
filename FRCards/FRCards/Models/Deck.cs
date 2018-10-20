using System;
using System.Collections.Generic;
using System.Text;

namespace FRCards.Models
{
    public class Deck
    {
        public RiderType Rider { get; set; }
        public Stack<Card> Cards { get; set; }
    }
}
