using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRCards.ViewModels
{
    public class DeckViewModel : BindableBase
    {
        private Deck model;
        public Deck Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }

        public CardViewModel NullCard { get; }

        public CardViewModel topCard = null;
        public CardViewModel TopCard
        {
            get => topCard;
            set => SetProperty(ref topCard, value);
        }

        private bool isFaceUp;
        public bool IsFaceUp
        {
            get => isFaceUp;
            set => SetProperty(ref isFaceUp, value);
        }

        public bool IsDiscard { get; }

        public DeckViewModel(bool isDiscard=false)
        {
            model = new Deck()
            {
                Cards = new Stack<Card>()
            };
            isFaceUp = false;
            this.IsDiscard = isDiscard;
            NullCard = new CardViewModel(null, isDiscard);
            setTopCard();
        }

        public int CardCount => Model.Cards.Count;
        public bool HasCards => CardCount > 0;

        public void Shuffle()
        {
            Stack<Card> newStack = new Stack<Card>();

            Random r = new Random();
            List<Card> cardList = model.Cards.ToList();

            while (cardList.Count > 0)
            {
                int cardIdx = r.Next(cardList.Count);
                newStack.Push(cardList[cardIdx]);
                cardList.RemoveAt(cardIdx);
            }

            model.Cards = newStack;
            setTopCard();
        }

        private void setTopCard()
        {
            if (HasCards)
                TopCard = new CardViewModel(model.Cards.Peek(), IsDiscard);
            else
                TopCard = NullCard;
        }

        public CardViewModel DrawTopCard()
        {
            CardViewModel retCard = TopCard;
            if (HasCards)
            {
                Model.Cards.Pop();
                setTopCard();
                OnPropertyChanged(nameof(CardCount));
                if (!HasCards)
                    OnPropertyChanged(nameof(HasCards));
            }

            return retCard;
        }

        public void AddCard(CardViewModel card)
        {
            bool prevHasCards = HasCards;
            Model.Cards.Push(card.Model);
            card.IsDiscarded = IsDiscard;
            TopCard = card;
            OnPropertyChanged(nameof(CardCount));
            if (prevHasCards != HasCards)
                OnPropertyChanged(nameof(HasCards));
        }

        public void AddCard(Card card)
        {
            bool prevHasCards = HasCards;
            Model.Cards.Push(card);
            setTopCard();
            OnPropertyChanged(nameof(CardCount));
            if (prevHasCards != HasCards)
                OnPropertyChanged(nameof(HasCards));
        }
    }
}
