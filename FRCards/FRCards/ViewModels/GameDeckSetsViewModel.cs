using System;
using System.Collections.Generic;
using System.Text;

namespace FRCards.ViewModels
{
    public class GameDeckSetsViewModel : BindableBase
    {
        private RouleurDeckSetViewModel rouleurSet;
        public RouleurDeckSetViewModel RouleurSet
        {
            get => rouleurSet;
            set => SetProperty(ref rouleurSet, value);
        }

        private SprinteurDeckSetViewModel sprinteurSet;
        public SprinteurDeckSetViewModel SprinteurSet
        {
            get => sprinteurSet;
            set => SetProperty(ref sprinteurSet, value);
        }

        public GameDeckSetsViewModel()
        {
            rouleurSet = new RouleurDeckSetViewModel();
            rouleurSet.CanDrawCards = true;
            rouleurSet.PropertyChanged += RouleurSet_PropertyChanged;

            sprinteurSet = new SprinteurDeckSetViewModel();
            sprinteurSet.CanDrawCards = true;
            sprinteurSet.PropertyChanged += SprinteurSet_PropertyChanged;
        }

        private void SprinteurSet_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RouleurSet_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
