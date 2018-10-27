using FRCards.ViewModels;
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
            for (int deckCard=ViewModel.CardCount; deckCard > 0; deckCard--)
            {
                Children.Add(new Frame()
                {
                    BorderColor = deckCard % 2 == 1 ? Color.DarkGray : Color.Black,
                    HeightRequest = 214,
                    WidthRequest = 94,
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.White,
                    TranslationX = deckCard - ViewModel.CardCount,
                    TranslationY = deckCard - ViewModel.CardCount
                });                    
            }
            CardFrame topCardFrame = new CardFrame()
            {
                BindingContext = ViewModel.TopCard,
                TranslationX = -ViewModel.CardCount,
                TranslationY = -ViewModel.CardCount,
            };
            topCardFrame.SetBinding(CardFrame.IsTappableProperty, new Binding("IsTappable", source: this));
            topCardFrame.SetBinding(CardFrame.IsFaceUpProperty, new Binding("BindingContext.IsFaceUp", source: this));
            Children.Add(topCardFrame);
        }
	}
}