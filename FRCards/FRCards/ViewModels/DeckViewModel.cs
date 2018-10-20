using FRCards.Models;
using System;
using System.Collections.Generic;
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
    }
}
