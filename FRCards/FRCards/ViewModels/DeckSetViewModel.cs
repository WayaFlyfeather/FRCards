using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FRCards.ViewModels
{
    public abstract class DeckSetViewModel : BindableBase
    {
        protected DeckSetViewModel()
        {
            displayExhaustionCard = GetExhaustionCard();
        }

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

        readonly CardViewModel displayExhaustionCard;
        public CardViewModel DisplayExhaustionCard => displayExhaustionCard; 

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

        private bool canFinishRound;
        public bool CanFinishRound
        {
            get => canFinishRound;
            set
            {
                if (SetProperty(ref canFinishRound, value))
                {
                    finishRoundCommand?.ChangeCanExecute();
                    finishRoundWithExhaustionCommand?.ChangeCanExecute();
                }
            }
        }

        public bool CanSelect => selectionCards != null;

        private CardViewModel[] selectionCards;
        public CardViewModel[] SelectionCards
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

        private CardViewModel selectedCard;
        public CardViewModel SelectedCard
        {
            get => selectedCard;
            set
            {
                if (SetProperty(ref selectedCard, value))
                {
                    OnPropertyChanged(nameof(HasSelectedCard));
                }
            }
        }

        public void DrawSelectionCards()
        {
            CardViewModel[] newSelectionCards = new CardViewModel[3];
            
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
            if (!CanSelect)
                return;

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
            UsedCards.IsFaceUp = false;
            UsedCards.Shuffle();
            ActiveDeck = UsedCards;
            UsedCards = new DeckViewModel() { IsFaceUp = true };
        }

        public bool HasSelectedCard => SelectedCard != null;

        public void FinishRound()
        {
            if (!CanFinishRound)
                return;

            Discarded.AddCard(SelectedCard);
            SelectedCard = null;
            CanFinishRound = false;
        }

        public void FinishRoundWithExhaustion()
        {
            if (!CanFinishRound)
                return;

            FinishRound();
            UsedCards.AddCard(GetExhaustionCard());
        }

        public abstract CardViewModel GetExhaustionCard();

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
