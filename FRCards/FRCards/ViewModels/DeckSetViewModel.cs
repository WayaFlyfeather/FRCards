﻿using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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


        private bool canDrawCards;
        public bool CanDrawCards
        {
            get => canDrawCards;
            set
            {
                if (SetProperty(ref canDrawCards, value))
                {
                    drawCardsCommand?.ChangeCanExecute();
                }
            }
        }

        public bool CanSelect => selectionCards != null;

        private Card[] selectionCards;
        public Card[] SelectionCards
        {
            get => selectionCards;
            set
            {
                if (SetProperty(ref selectionCards, value))
                {
                    OnPropertyChanged(nameof(CanSelect));
                    selectCardCommand?.ChangeCanExecute();
                }
            }
        }

        private Card selectedCard;
        public Card SelectedCard
        {
            get => selectedCard;
            set
            {
                if (SetProperty(ref selectedCard, value))
                {
                    OnPropertyChanged(nameof(HasSelectedCard));
                    finishRoundCommand?.ChangeCanExecute();
                    finishRoundWithExhaustionCommand?.ChangeCanExecute();
                }
            }
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
            CanDrawCards = false;
            SelectionCards = newSelectionCards;
        }

        public void SelectCard(int selection)
        {
            for (int idx=0; idx < 3; idx++)
            {
                if (idx == selection)
                    SelectedCard = SelectionCards[idx];
                else
                    UsedCards.AddCard(SelectionCards[idx]);
            }

            SelectionCards = null;
        }

        public void MakeUsedDeckActive()
        {
            UsedCards.Shuffle();
            ActiveDeck = UsedCards;
            UsedCards = new DeckViewModel();
        }

        public bool HasSelectedCard => SelectedCard != null;

        public void FinishRound()
        {
            Discarded.AddCard(SelectedCard);
            SelectedCard = null;
        }

        public void FinishRoundWithExhaustion()
        {
            FinishRound();
            UsedCards.AddCard(GetExhaustionCard());
        }

        public abstract Card GetExhaustionCard();

        private Command drawCardsCommand;
        public ICommand DrawCardsCommand => drawCardsCommand ?? (drawCardsCommand = new Command(DrawSelectionCards, () => CanDrawCards));

        private Command<string> selectCardCommand;
        public ICommand SelectCardCommand => selectCardCommand ?? (selectCardCommand = new Command<string>(cardNo => SelectCard(int.Parse(cardNo)), cardNo => CanSelect));

        private Command finishRoundCommand;
        public ICommand FinishRoundCommand => finishRoundCommand ?? (finishRoundCommand = new Command(FinishRound, () => HasSelectedCard));

        private Command finishRoundWithExhaustionCommand;
        public ICommand FinishRoundWithExhaustionCommand => finishRoundWithExhaustionCommand ?? (finishRoundWithExhaustionCommand = new Command(FinishRoundWithExhaustion, () => HasSelectedCard));
    }
}
