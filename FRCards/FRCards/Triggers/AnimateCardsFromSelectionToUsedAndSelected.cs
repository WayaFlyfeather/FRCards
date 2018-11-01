﻿using FRCards.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FRCards.Triggers
{
    public class AnimateCardsFromSelectionToUsedAndSelected : TriggerAction<DeckSetView>
    {
        protected async override void Invoke(DeckSetView sender)
        {
            await sender.MoveCardsFromSelection();
        }
    }
}
