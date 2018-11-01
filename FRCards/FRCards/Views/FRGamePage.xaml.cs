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

		public FRGamePage ()
		{
			InitializeComponent ();
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