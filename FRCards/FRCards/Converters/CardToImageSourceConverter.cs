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
            Assembly thisAssembly = GetType().Assembly;

            Card card = value as Card;

            if (card == null)  //Todo: replace with empty card image.
                return ImageSource.FromResource("FRCards.Assets.RBack_200h.png", thisAssembly);

            bool faceUp = true;
            if (parameter != null)
                faceUp = System.Convert.ToBoolean(parameter);

            if (card.Rider==RiderType.Rouleur)
            {
                if (!faceUp)
                    return ImageSource.FromResource("FRCards.Assets.RBack_200h.png", thisAssembly);

                if (card.IsExhaustion)
                    return ImageSource.FromResource("FRCards.Assets.RExh_200h.png", thisAssembly);

                switch (card.Movement)
                {
                    case 3: return ImageSource.FromResource("FRCards.Assets.R3_200h.png", thisAssembly);
                    case 4: return ImageSource.FromResource("FRCards.Assets.R4_200h.png", thisAssembly);
                    case 5: return ImageSource.FromResource("FRCards.Assets.R5_200h.png", thisAssembly);
                    case 6: return ImageSource.FromResource("FRCards.Assets.R6_200h.png", thisAssembly);
                    case 7: return ImageSource.FromResource("FRCards.Assets.R7_200h.png", thisAssembly);
                }
            }

            if (card.Rider == RiderType.Sprinteur)
            {
                if (!faceUp)
                    return ImageSource.FromResource("FRCards.Assets.SBack_200h.png", thisAssembly);

                if (card.IsExhaustion)
                    return ImageSource.FromResource("FRCards.Assets.SExh_200h.png", thisAssembly);

                switch (card.Movement)
                {
                    case 2: return ImageSource.FromResource("FRCards.Assets.S2_200h.png", thisAssembly);
                    case 3: return ImageSource.FromResource("FRCards.Assets.S3_200h.png", thisAssembly);
                    case 4: return ImageSource.FromResource("FRCards.Assets.S4_200h.png", thisAssembly);
                    case 5: return ImageSource.FromResource("FRCards.Assets.S5_200h.png", thisAssembly);
                    case 9: return ImageSource.FromResource("FRCards.Assets.S9_200h.png", thisAssembly);
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
