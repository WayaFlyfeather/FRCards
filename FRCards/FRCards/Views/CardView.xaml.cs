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
    public partial class CardView : ContentView
    {
        public bool IsTappable
        {
            get => (bool)GetValue(IsTappableProperty);
            set => SetValue(IsTappableProperty, value);
        }
        public static readonly BindableProperty IsTappableProperty = BindableProperty.Create(nameof(IsTappable), typeof(bool), typeof(CardView), false);

        public bool IsFaceUp
        {
            get => (bool)GetValue(IsFaceUpProperty);
            set => SetValue(IsFaceUpProperty, value);
        }
        public static readonly BindableProperty IsFaceUpProperty = BindableProperty.Create(nameof(IsFaceUp), typeof(bool), typeof(CardView), true);

        public bool IsVisibleWithoutAnimation
        {
            get => (bool)GetValue(IsVisibleWithoutAnimationProperty);
            set => SetValue(IsVisibleWithoutAnimationProperty, value);
        }
        public static readonly BindableProperty IsVisibleWithoutAnimationProperty = BindableProperty.Create(nameof(IsVisibleWithoutAnimation), typeof(bool), typeof(CardView), true);

        public CardView()
        {
            InitializeComponent();
        }

        public void PrepareAnimateMove(double moveX, double moveY, bool flip)
        {
            cardFrame.TranslationX = -moveX;
            cardFrame.TranslationY = -moveY;

            if (flip)
                IsFaceUp = !IsFaceUp;

            cardFrame.IsVisible = true;
        }

        public Task<bool> AnimateMove(uint length=250)
        {
            return cardFrame.TranslateTo(0.0, 0.0, length);
        }

        public async Task<bool> AnimateFlipAndMove(uint length=250)
        {
            Task moveTask = AnimateMove(length);

            await cardFrame.RotateYTo(90.0, length / 2);

            IsFaceUp = !IsFaceUp;
            cardFrame.RotationY = 270;

            await cardFrame.RotateYTo(360.0, length / 2);

            cardFrame.RotationY = 0;
            await moveTask;

            return true;
        }

        public void HideTillNextAnimation()
        {
            cardFrame.IsVisible = false;
        }
    }
}