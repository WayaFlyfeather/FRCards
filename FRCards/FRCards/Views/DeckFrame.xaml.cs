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
	public partial class DeckFrame : Frame
	{
        DeckViewModel ViewModel => BindingContext as DeckViewModel;
        DeckViewModel prevViewModel = null;

        public bool IsTappable
        {
            get => (bool)GetValue(IsTappableProperty);
            set => SetValue(IsTappableProperty, value);
        }
        public static readonly BindableProperty IsTappableProperty = BindableProperty.Create(nameof(IsTappable), typeof(bool), typeof(DeckFrame), false);

        public DeckFrame ()
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

            deckGrid.Children.Clear();
            for (int deckCard=ViewModel.CardCount; deckCard > 0; deckCard--)
            {
                deckGrid.Children.Add(new Frame()
                {
                    BorderColor = Color.Black,
                    HeightRequest = 214,
                    WidthRequest = 94,
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.White,
                    TranslationX = (ViewModel.CardCount - deckCard) * -2,
                    TranslationY = (ViewModel.CardCount - deckCard) * -2,
                });                    
            }
            CardFrame topCardFrame = new CardFrame()
            {
                BindingContext = ViewModel.TopCard,
                TranslationX = ViewModel.CardCount * -2,
                TranslationY = ViewModel.CardCount * -2,
            };
            topCardFrame.SetBinding(CardFrame.IsTappableProperty, new Binding("IsTappable", source: this));
            topCardFrame.SetBinding(CardFrame.IsFaceUpProperty, new Binding("BindingContext.IsFaceUp", source: this));
            deckGrid.Children.Add(topCardFrame);
        }
	}
}