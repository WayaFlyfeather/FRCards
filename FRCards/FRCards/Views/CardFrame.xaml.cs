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
    public partial class CardFrame : Frame
    {
        public bool IsTappable
        {
            get => (bool)GetValue(IsTappableProperty);
            set => SetValue(IsTappableProperty, value);
        }

        public static readonly BindableProperty IsTappableProperty = BindableProperty.Create(nameof(IsTappable), typeof(bool), typeof(CardFrame), false);

        public CardFrame()
        {
            InitializeComponent();
        }
    }
}