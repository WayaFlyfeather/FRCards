﻿using FRCards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FRCards.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeckGrid : Grid
	{
        DeckViewModel ViewModel => BindingContext as DeckViewModel;
        DeckViewModel prevViewModel = null;

        public bool IsTappable
        {
            get => (bool)GetValue(IsTappableProperty);
            set => SetValue(IsTappableProperty, value);
        }
        public static readonly BindableProperty IsTappableProperty = BindableProperty.Create(nameof(IsTappable), typeof(bool), typeof(DeckGrid), false);

        public DeckGrid ()
		{
			InitializeComponent ();
            OnBindingContextChanged();
		}

        protected override void OnBindingContextChanged()
        {
            if (prevViewModel != null)
                prevViewModel.PropertyChanged -= ViewModel_PropertyChanged;

            constructDeck();
            if (ViewModel != null)
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            prevViewModel = ViewModel;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.PropertyName) || e.PropertyName == nameof(DeckViewModel.CardCount))
                constructDeck();
        }

        void constructDeck()
        {
            if (ViewModel is null)
                return;

            Children.Clear();
            Grid deckGrid = new Grid();
            for (int deckCard = ViewModel.CardCount; deckCard > 0; deckCard--)
            {
                deckGrid.Children.Add(new Frame()
                {
                    BorderColor = deckCard % 2 == 1 ? Color.DarkGray : Color.Black,
                    HeightRequest = 200,
                    WidthRequest = 125,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Padding = new Thickness(6),
                    BackgroundColor = Color.White,
                    TranslationX = deckCard - ViewModel.CardCount,
                    TranslationY = deckCard - ViewModel.CardCount
                });
            }
            Children.Add(deckGrid);
            CardView topCardView = new CardView()
            {
                TranslationX = -ViewModel.CardCount,
                TranslationY = -ViewModel.CardCount,
            };
            topCardView.SetBinding(CardView.BindingContextProperty, "TopCard");
            topCardView.SetBinding(CardView.IsTappableProperty, new Binding("IsTappable", source: this));
            topCardView.SetBinding(CardView.IsFaceUpProperty, new Binding("BindingContext.IsFaceUp", source: this));
            Children.Add(topCardView);
        }

        public async Task<bool> AnimateCardIn(CardViewModel card, double FromX, double FromY, uint length=250)
        {
            CardView animateCard = new CardView()
            {
                TranslationX = -ViewModel.CardCount,
                TranslationY = -ViewModel.CardCount,
                BindingContext = card,
                IsTappable = false,
                IsFaceUp = true,
                IsVisibleWithoutAnimation = false
            };
            Children.Add(animateCard);
            animateCard.PrepareAnimateMove(this.X - FromX, this.Y - FromY, false);
            await animateCard.AnimateMove(length);
            Children.Remove(animateCard);
            ViewModel.AddAfterAnimation(card);

            return true;
        }
	}
}