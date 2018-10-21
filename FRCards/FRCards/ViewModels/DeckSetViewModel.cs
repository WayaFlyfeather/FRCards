using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRCards.ViewModels
{
    public abstract class DeckSetViewModel : BindableBase
    {

        private DeckViewModel activeDeck;
        public DeckViewModel ActiveDeck
        {
            get => activeDeck;
            set => SetProperty(ref activeDeck, value);
        }

        private DeckViewModel usedCards;
        public DeckViewModel UsedCards
        {
            get => usedCards;
            set => SetProperty(ref usedCards, value);
        }


        private DeckViewModel discarded;
        public DeckViewModel Discarded
        {
            get => discarded;
            set => SetProperty(ref discarded, value);
        }

        private Card[] selectionCards;
        public Card[] SelectionCards
        {
            get => selectionCards;
            set => SetProperty(ref selectionCards, value);
        }


        private Card selectedCard;
        public Card SelectedCard
        {
            get => selectedCard;
            set => SetProperty(ref selectedCard, value);
        }

        public void DrawSelectionCards()
        {
            Card[] newSelectionCards = new Card[3];
            
            for (int idx=0; idx < 3; idx++)
            {
                if (!ActiveDeck.HasCards)
                    MakeUsedDeckActive();

                newSelectionCards[idx] = ActiveDeck.HasCards ? ActiveDeck.DrawTopCard() : GetExhaustionCard();
            }
        }

        public void SelectCard(int selection)
        {
            for (int idx=0; idx < 3; idx++)
            {
                if (idx == selection)
                    SelectedCard = SelectionCards[idx];
                else
                    UsedCards.Model.Cards.Push(SelectionCards[idx]);
            }

            SelectionCards = null;
        }

        public void MakeUsedDeckActive()
        {
            UsedCards.Shuffle();
            ActiveDeck = UsedCards;
            UsedCards = new DeckViewModel();
        }

        public void FinishRound()
        {
            Discarded.Model.Cards.Push(SelectedCard);
            SelectedCard = null;
        }

        public void FinishRoundWithExhaustion()
        {
            FinishRound();
            UsedCards.Model.Cards.Push(GetExhaustionCard());
        }

        public abstract DeckViewModel CreateNewDeck();
        public abstract Card GetExhaustionCard();
    }
}
