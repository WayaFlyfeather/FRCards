using System;
using System.Collections.Generic;
using System.Text;

namespace FRCards.ViewModels
{
    public static class DesignTimeViewModels
    {
        static RouleurDeckSetViewModel designRouleurDeckSetViewModel = null;
        public static RouleurDeckSetViewModel DesignRouleurDeckSetViewModel => designRouleurDeckSetViewModel ?? (designRouleurDeckSetViewModel = new RouleurDeckSetViewModel());
    }
}
