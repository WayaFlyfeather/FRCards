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
	public partial class FRGamePage : ContentPage
	{
        public GameDeckSetsViewModel ViewModel => BindingContext as GameDeckSetsViewModel;

        double translating;
        double scaling;
        public double Scaling => scaling;

		public FRGamePage ()
		{
			InitializeComponent();
            SizeChanged += FRGamePage_SizeChanged;
            sizeChanged();
            OnBindingContextChanged();
		}

        private void FRGamePage_SizeChanged(object sender, EventArgs e)
        {
            sizeChanged();
        }

        private void sizeChanged()
        {
            translating = (Width - 1020.0) / 3.5;
            double scalingX = Width / 1020.0;
            double scalingY = Height / 670.0;

            scaling = Math.Min(scalingX, scalingY);
            foreach (View v in gameGrid.Children)
            {
                DeckSetView dsv = v as DeckSetView;
                if (dsv != null)
                    dsv.SetInnerScale(scaling, translating);
            }
//            Scale = scaling;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            DeckSetView rouleurDeckSetView = new DeckSetView()
            {
                Rider = "Rouleur",
                //Scale = scaling
            };
            rouleurDeckSetView.SetBinding(DeckSetView.BindingContextProperty, "RouleurSet");
            rouleurDeckSetView.SetInnerScale(scaling, translating);

            DeckSetView sprinteurDeckSetView = new DeckSetView()
            {
                Rider = "Sprinteur",
//                Scale = scaling
            };
            sprinteurDeckSetView.SetBinding(DeckSetView.BindingContextProperty, "SprinteurSet");
            sprinteurDeckSetView.SetInnerScale(scaling, translating);

            foreach (View v in gameGrid.Children)
            {
                v.BindingContext = null;
            }
            gameGrid.Children.Clear();

            gameGrid.Children.Add(rouleurDeckSetView, 0, 0);
            gameGrid.Children.Add(sprinteurDeckSetView, 0, 1);
        }

        public async Task RequestGameReset()
        {
            if (await DisplayAlert("Restart Game?", "Are you sure you want to start a new game?", "Yes", "No")==true)
                BindingContext = new GameDeckSetsViewModel();
            else
            {
                ViewModel.RouleurSet.GameResetRequested = false;
                ViewModel.SprinteurSet.GameResetRequested = false;
            }
        }
	}
}