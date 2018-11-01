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

        public DeckSetView()
        {
            InitializeComponent();
        }

        bool isAnimating = false;
        public async Task MoveSelectionCardsFromDeckToSelection()
        {
            if (isAnimating)
                return;
            isAnimating = true;

            selectionCardsGrid.IsVisible = true;
            selectionCardView0.BindingContext = ViewModel.SelectionCards[0];
            selectionCardView1.BindingContext = ViewModel.SelectionCards[1];
            selectionCardView2.BindingContext = ViewModel.SelectionCards[2];

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

        public async Task MoveCardsFromSelection()
        {
            if (isAnimating)
                return;
            isAnimating = true;

            selectedCardView.HideTillNextAnimation();
            selectedCardView.IsVisible = true;
            await moveCardsFromSelectionToUsedCards();
            await moveCardToSelectedCard();
            selectionCardsGrid.IsVisible = false;
            selectionCardView0.BindingContext = null;
            selectionCardView1.BindingContext = null;
            selectionCardView2.BindingContext = null;

            isAnimating = false;
        }

        public async Task MoveToFinishPositionsAsync()
        {
            await Task.Delay(1000); //allow previous animations time to finish

            if (isAnimating)
                return;
            isAnimating = true;

            selectedCardView.HideTillNextAnimation();

            finishedCardWithExhaustion.HideTillNextAnimation();
            finishedCard.HideTillNextAnimation();
            finishedExhaustionCard.Opacity = 0.0;

            finishingCardGrid.IsVisible = true;

            finishedCardWithExhaustion.PrepareAnimateMove(finishingCardGrid.X + finishedCardWithExhaustion.X - selectedCardView.X, finishingCardGrid.Y + finishedCardWithExhaustion.Y - selectedCardView.Y, false);
            finishedCard.PrepareAnimateMove(finishingCardGrid.X + finishedCard.X - selectedCardView.X, finishingCardGrid.Y + finishedCard.Y - selectedCardView.Y, false);
            Task t1 = finishedCardWithExhaustion.AnimateMove(100);
            Task t2 = finishedCard.AnimateMove(100);

            selectedCardView.IsVisible = false;

            await Task.WhenAll(t1, t2);

            await finishedExhaustionCard.FadeTo(1.0, 100);

            isAnimating = false;
        }

        public async Task MoveFinishCardsToDeck()
        {
            if (isAnimating)
                return;
            isAnimating = true;

            await Task.Delay(50);
            Task finishAnimationsTask;

            if (ViewModel.UsedCards.HasWaitingAnimations)
            {
                CardViewModel animateExhaustionCard = ViewModel.UsedCards.GetNextCardForAnimation();
                CardViewModel animateFinishedCard = ViewModel.Discarded.GetNextCardForAnimation();

                Task t1 = usedDeckGrid.AnimateCardIn(animateExhaustionCard, finishingCardGrid.X + finishedExhaustionCard.X, finishingCardGrid.Y + finishedExhaustionCard.Y, 250);
                Task t2 = discardedDeckGrid.AnimateCardIn(animateFinishedCard, finishingCardGrid.X + finishedCardWithExhaustion.X, finishingCardGrid.Y + finishedCardWithExhaustion.Y, 250);
                finishAnimationsTask= Task.WhenAll(t1, t2);
            }
            else
            {
                CardViewModel animateFinishedCard = ViewModel.Discarded.GetNextCardForAnimation();

                finishAnimationsTask = discardedDeckGrid.AnimateCardIn(animateFinishedCard, finishingCardGrid.X + finishedCard.X, finishingCardGrid.Y + finishedCard.Y, 250);
            }

            finishedExhaustionCard.Opacity = 0.0;
            finishedCardWithExhaustion.HideTillNextAnimation();
            finishedCard.HideTillNextAnimation();

            finishingCardGrid.IsVisible = false;

            await finishAnimationsTask;

            isAnimating = false;
        }

        async Task moveCardsFromSelectionToUsedCards()
        {
            while (ViewModel.UsedCards.HasWaitingAnimations)
            {
                CardViewModel animateCard = ViewModel.UsedCards.GetNextCardForAnimation();

                if (animateCard == selectionCardView0.BindingContext)
                {
                    selectionCardView0.HideTillNextAnimation();
                    await usedDeckGrid.AnimateCardIn(animateCard, selectionCardsGrid.X + selectionCardView0.X, selectionCardsGrid.Y + selectionCardView0.Y, 250);
                    selectionCardView0.BindingContext = null;
                }
                else if (animateCard == selectionCardView1.BindingContext)
                {
                    selectionCardView1.HideTillNextAnimation();
                    await usedDeckGrid.AnimateCardIn(animateCard, selectionCardsGrid.X + selectionCardView1.X, selectionCardsGrid.Y + selectionCardView1.Y, 250);
                    selectionCardView1.BindingContext = null;
                }
                else if (animateCard == selectionCardView2.BindingContext)
                {
                    selectionCardView2.HideTillNextAnimation();
                    await usedDeckGrid.AnimateCardIn(animateCard, selectionCardsGrid.X + selectionCardView2.X, selectionCardsGrid.Y + selectionCardView2.Y, 250);
                    selectionCardView2.BindingContext = null;
                }
                else
                    ViewModel.UsedCards.AddAfterAnimation(animateCard);
            }
        }

        async Task moveCardToSelectedCard()
        {
            if (selectionCardView0.BindingContext != null)
            {
                selectedCardView.PrepareAnimateMove(selectedCardView.X - (selectionCardsGrid.X + selectionCardView0.X), selectedCardView.Y - (selectionCardsGrid.Y + selectionCardView0.Y), false);
                selectionCardView0.HideTillNextAnimation();
                selectionCardView0.BindingContext = null;
                await selectedCardView.AnimateMove(50);
            }
            else if (selectionCardView1.BindingContext != null)
            {
                selectedCardView.PrepareAnimateMove(selectedCardView.X - (selectionCardsGrid.X + selectionCardView1.X), selectedCardView.Y - (selectionCardsGrid.Y + selectionCardView1.Y), false);
                selectionCardView1.HideTillNextAnimation();
                selectionCardView1.BindingContext = null;
                await selectedCardView.AnimateMove(50);
            }
            else if (selectionCardView2.BindingContext != null)
            {
                selectedCardView.PrepareAnimateMove(selectedCardView.X - (selectionCardsGrid.X + selectionCardView2.X), selectedCardView.Y - (selectionCardsGrid.Y + selectionCardView2.Y), false);
                selectionCardView2.HideTillNextAnimation();
                selectionCardView2.BindingContext = null;
                await selectedCardView.AnimateMove(50);
            }
        }

    }
}