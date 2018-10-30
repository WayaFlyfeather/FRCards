using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRCards.ViewModels
{
    public class CardViewModel : BindableBase
    {        
        private Card model;
        public Card Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }

        private bool isDiscarded;
        public bool IsDiscarded
        {
            get => isDiscarded;
            set => SetProperty(ref isDiscarded, value);
        }

        public CardViewModel(Card model, bool isDiscarded=false)
        {
            this.model = model;
            this.isDiscarded = isDiscarded;
        }
    }
}
