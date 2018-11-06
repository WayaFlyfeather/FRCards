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

        double scaling;

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
            double scalingX = Width / 1080.0;
            double scalingY = Height / 780.0;

            scaling = Math.Min(scalingX, scalingY);
            foreach (View v in gameGrid.Children)
            {
                DeckSetView dsv = v as DeckSetView;
                if (dsv != null)
                    dsv.SetInnerScale(scaling);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            DeckSetView rouleurDeckSetView = new DeckSetView()
            {
                Rider = "Rouleur"
            };
            rouleurDeckSetView.SetBinding(DeckSetView.BindingContextProperty, "RouleurSet");
            rouleurDeckSetView.SetInnerScale(scaling);

            DeckSetView sprinteurDeckSetView = new DeckSetView()
            {
                Rider = "Sprinteur"
            };
            sprinteurDeckSetView.SetBinding(DeckSetView.BindingContextProperty, "SprinteurSet");
            sprinteurDeckSetView.SetInnerScale(scaling);

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