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

        public CardView()
        {
            InitializeComponent();
        }

        public Task<bool> AnimateMove(double moveX, double moveY)
        {
            cardFrame.TranslationX = -moveX;
            cardFrame.TranslationY = -moveY;
            return cardFrame.TranslateTo(0.0, 0.0);
        }

        public async Task<bool> AnimateFlipAndMove(double moveX, double moveY)
        {
            Task moveTask = AnimateMove(moveX, moveY);

            await cardFrame.RotateYTo(90.0, 125);

            IsFaceUp = !IsFaceUp;
            cardFrame.RotationY = 270;

            await cardFrame.RotateYTo(360.0, 125);

            cardFrame.RotationY = 0;
            await moveTask;

            return true;
        }
    }
}