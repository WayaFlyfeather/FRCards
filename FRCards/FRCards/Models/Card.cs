using System;
using System.Collections.Generic;
using System.Text;

namespace FRCards.Models
{
    public enum RiderType { Rouleur, Sprinteur}
    public class Card
    {
        public int Movement { get; set; }
        public RiderType Rider { get; set; }
        public bool IsExhaustion { get; set; }
    }
}
