using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace FRCards.Converters
{
    public class CardToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Card card = value as Card;

            if (card == null)
                return null;

            Assembly thisAssembly = GetType().Assembly;
            if (card.Rider==RiderType.Rouleur)
            {
                if (card.IsExhaustion)
                    return ImageSource.FromResource("FRCards.Assets.RExh.png", thisAssembly);

                switch (card.Movement)
                {
                    case 3: return ImageSource.FromResource("FRCards.Assets.R3.png", thisAssembly);
                    case 4: return ImageSource.FromResource("FRCards.Assets.R4.png", thisAssembly);
                    case 5: return ImageSource.FromResource("FRCards.Assets.R5.png", thisAssembly);
                    case 6: return ImageSource.FromResource("FRCards.Assets.R6.png", thisAssembly);
                    case 7: return ImageSource.FromResource("FRCards.Assets.R7.png", thisAssembly);
                }
            }

            if (card.Rider == RiderType.Sprinteur)
            {
                if (card.IsExhaustion)
                    return ImageSource.FromResource("FRCards.Assets.SExh.png", thisAssembly);

                switch (card.Movement)
                {
                    case 2: return ImageSource.FromResource("FRCards.Assets.S2.png", thisAssembly);
                    case 3: return ImageSource.FromResource("FRCards.Assets.S3.png", thisAssembly);
                    case 4: return ImageSource.FromResource("FRCards.Assets.S4.png", thisAssembly);
                    case 5: return ImageSource.FromResource("FRCards.Assets.S5.png", thisAssembly);
                    case 9: return ImageSource.FromResource("FRCards.Assets.S9.png", thisAssembly);
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
