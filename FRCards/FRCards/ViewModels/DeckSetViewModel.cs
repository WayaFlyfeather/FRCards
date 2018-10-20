using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRCards.ViewModels
{
    public class DeckSetViewModel : BindableBase
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
    }
}
