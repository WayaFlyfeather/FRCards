using System;
using System.Collections.Generic;
using System.Text;
using FRCards.Models;

namespace FRCards.ViewModels
{
    public class RouleurDeckSetViewModel : DeckSetViewModel
    {
        public override Card GetExhaustionCard()
        {
            return new Card()
            {
                Movement = 2,
                Rider = RiderType.Rouleur,
                IsExhaustion = true
            };
        }

        public RouleurDeckSetViewModel() : base()
        {
            UsedCards = new DeckViewModel();
            ActiveDeck = new DeckViewModel();
            Discarded = new DeckViewModel();
            SelectionCards = null;
            SelectedCard = null;

            for (int count = 0; count < 3; count++)
                ActiveDeck.Model.Cards.Push(new Card { Movement = 3, Rider = RiderType.Rouleur, IsExhaustion = false });
            for (int count = 0; count < 3; count++)
                ActiveDeck.Model.Cards.Push(new Card { Movement = 4, Rider = RiderType.Rouleur, IsExhaustion = false });
            for (int count = 0; count < 3; count++)
                ActiveDeck.Model.Cards.Push(new Card { Movement = 5, Rider = RiderType.Rouleur, IsExhaustion = false });
            for (int count = 0; count < 3; count++)
                ActiveDeck.Model.Cards.Push(new Card { Movement = 6, Rider = RiderType.Rouleur, IsExhaustion = false });
            for (int count = 0; count < 3; count++)
                ActiveDeck.Model.Cards.Push(new Card { Movement = 7, Rider = RiderType.Rouleur, IsExhaustion = false });

            ActiveDeck.Shuffle();
        }
    }
}
