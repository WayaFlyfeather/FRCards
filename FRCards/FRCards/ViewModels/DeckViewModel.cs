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

        public DeckViewModel()
        {
            model = new Deck()
            {
                Cards = new Stack<Card>()
            };
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
        }

        public Card DrawTopCard()
        {
            if (HasCards)
            {
                Card drawnCard = Model.Cards.Pop();
                OnPropertyChanged(nameof(CardCount));
                OnPropertyChanged(nameof(HasCards));

                return drawnCard;
            }

            return null; // throw exception?
        }
    }
}
