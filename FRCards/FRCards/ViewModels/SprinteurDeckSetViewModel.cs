using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRCards.ViewModels
{
    public class SprinteurDeckSetViewModel : DeckSetViewModel
    {
        public override Card GetExhaustionCard()
        {
            return new Card()
            {
                Movement = 2,
                Rider = RiderType.Sprinteur,
                IsExhaustion = true
            };
        }

        public SprinteurDeckSetViewModel() : base()
        {
            UsedCards = new DeckViewModel();
            ActiveDeck = new DeckViewModel();
            Discarded = new DeckViewModel();
            SelectionCards = null;
            SelectedCard = null;

            for (int count = 0; count < 3; count++)
                ActiveDeck.Model.Cards.Push(new Card { Movement = 2, Rider = RiderType.Sprinteur, IsExhaustion = false });
            for (int count = 0; count < 3; count++)
                ActiveDeck.Model.Cards.Push(new Card { Movement = 3, Rider = RiderType.Sprinteur, IsExhaustion = false });
            for (int count = 0; count < 3; count++)
                ActiveDeck.Model.Cards.Push(new Card { Movement = 4, Rider = RiderType.Sprinteur, IsExhaustion = false });
            for (int count = 0; count < 3; count++)
                ActiveDeck.Model.Cards.Push(new Card { Movement = 5, Rider = RiderType.Sprinteur, IsExhaustion = false });
            for (int count = 0; count < 3; count++)
                ActiveDeck.Model.Cards.Push(new Card { Movement = 9, Rider = RiderType.Sprinteur, IsExhaustion = false });

            ActiveDeck.Shuffle();
        }

    }
}
