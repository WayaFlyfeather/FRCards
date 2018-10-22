using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void SprinteurSet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.PropertyName) || e.PropertyName==nameof(SprinteurDeckSetViewModel.CanSelect))
            {
                if (SprinteurSet.CanSelect)
                    RouleurSet.CanDrawCards = false;
            }

            if (String.IsNullOrEmpty(e.PropertyName) || e.PropertyName == nameof(SprinteurDeckSetViewModel.HasSelectedCard))
            {
                if (SprinteurSet.HasSelectedCard && !RouleurSet.HasSelectedCard)
                    RouleurSet.CanDrawCards = true;
                else if (!RouleurSet.HasSelectedCard && !SprinteurSet.HasSelectedCard)
                {
                    SprinteurSet.CanDrawCards = true;
                    RouleurSet.CanDrawCards = true;
                }
            }
        }

        private void RouleurSet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.PropertyName) || e.PropertyName == nameof(RouleurDeckSetViewModel.CanSelect))
            {
                if (RouleurSet.CanSelect)
                    SprinteurSet.CanDrawCards = false;
            }

            if (String.IsNullOrEmpty(e.PropertyName) || e.PropertyName == nameof(RouleurDeckSetViewModel.HasSelectedCard))
            {
                if (RouleurSet.HasSelectedCard && !SprinteurSet.HasSelectedCard)
                    SprinteurSet.CanDrawCards = true;
                else if (!RouleurSet.HasSelectedCard && !SprinteurSet.HasSelectedCard)
                {
                    SprinteurSet.CanDrawCards = true;
                    RouleurSet.CanDrawCards = true;
                }
            }
        }
    }
}
