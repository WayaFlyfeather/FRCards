using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRCards.ViewModels
{
    public static class DesignTimeViewModels
    {
        static RouleurDeckSetViewModel designRouleurDeckSetViewModel = null;
        public static RouleurDeckSetViewModel DesignRouleurDeckSetViewModel => designRouleurDeckSetViewModel ?? (designRouleurDeckSetViewModel = new RouleurDeckSetViewModel());

        static Card designTimeCard = null;
        public static Card DesignTimeCard => designTimeCard ?? (designTimeCard=new Card() { Movement = 3, IsExhaustion = false, Rider = RiderType.Rouleur } );
    }
}
