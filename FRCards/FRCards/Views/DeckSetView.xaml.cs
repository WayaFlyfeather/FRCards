using FRCards.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FRCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckSetView : ContentView
    {
        public string Rider
        {
            get => (string)GetValue(RiderProperty);
            set => SetValue(RiderProperty, value);
        }

        public static readonly BindableProperty RiderProperty = BindableProperty.Create("Rider", typeof(string), typeof(DeckSetView), "");

        public DeckSetViewModel ViewModel => this.BindingContext as DeckSetViewModel;
        DeckSetViewModel prevViewModel = null;

        public DeckSetView()
        {
            InitializeComponent();
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            if (prevViewModel != null)
                ViewModel.PropertyChanged -= ViewModel_PropertyChanged;

            base.OnBindingContextChanged();

            if (ViewModel != null)
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.PropertyName) || e.PropertyName==nameof(DeckSetViewModel.SelectionCards))
            {
                if (ViewModel?.SelectionCards != null)
                {
                    Debug.WriteLine("Property changed, property " + e.PropertyName);
                    await moveSelectionCardsFromDeckToSelection();
                }
                else
                    hideSelectionCardsTillNextAnimation();
            }
        }

        void hideSelectionCardsTillNextAnimation()
        {
            selectionCardView0.HideTillNextAnimation();
            selectionCardView1.HideTillNextAnimation();
            selectionCardView2.HideTillNextAnimation();
        }

        bool isAnimating = false;
        async Task moveSelectionCardsFromDeckToSelection()
        {
            if (isAnimating)
                return;
            isAnimating = true;

            await Task.Delay(50);

            selectionCardView0.PrepareAnimateMove(selectionCardsGrid.X + selectionCardView0.X - activeDeckGrid.X, (selectionCardsGrid.Y + selectionCardView0.Y) - activeDeckGrid.Y, true);
            selectionCardView1.PrepareAnimateMove(selectionCardsGrid.X + selectionCardView1.X - activeDeckGrid.X, (selectionCardsGrid.Y + selectionCardView1.Y) - activeDeckGrid.Y, true);
            selectionCardView2.PrepareAnimateMove(selectionCardsGrid.X + selectionCardView2.X - activeDeckGrid.X, (selectionCardsGrid.Y + selectionCardView2.Y) - activeDeckGrid.Y, true);

            Task a2 = selectionCardView2.AnimateFlipAndMove(250);
            await Task.Delay(80);
            Task a1 = selectionCardView1.AnimateFlipAndMove(250);
            await Task.Delay(80);
            Task a0 = selectionCardView0.AnimateFlipAndMove(250);

            await Task.WhenAll(a2, a1, a0);

            isAnimating = false;
        }
    }
}