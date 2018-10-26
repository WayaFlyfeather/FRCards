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

        static DeckViewModel designTimeDeck = null;
        public static DeckViewModel DesignTimeDeck => designTimeDeck ?? (designTimeDeck = new DeckViewModel() { Model = makeDesignTimeDeck() });

        static Deck makeDesignTimeDeck()
        {
            Deck deck = new Deck();

            deck.Cards = new Stack<Card>();
            deck.Cards.Push(new Card() { Movement = 3, Rider = RiderType.Rouleur });
            deck.Cards.Push(new Card() { Movement = 4, Rider = RiderType.Rouleur });
            deck.Cards.Push(new Card() { Movement = 5, Rider = RiderType.Rouleur });

            return deck;
        }
    }
}
