using FRCards.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FRCards.Triggers
{
    public class RequestGameResetAction : TriggerAction<FRGamePage>
    {
        protected async override void Invoke(FRGamePage sender)
        {
            await sender.RequestGameReset();
        }
    }
}
