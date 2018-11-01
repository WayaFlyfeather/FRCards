using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace FRCards.Converters
{
    public class RiderTypeToBackgroundColorConverter : IValueConverter
    {
        static Color RouleurColor { get; } = new Color(0.8, 0.2, 0.2, 0.2);
        static Color SprinteurColor { get; } = new Color(0.2, 0.2, 0.8, 0.2);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
                return Color.White;

            RiderType rider = (RiderType)value;

            if (rider == RiderType.Rouleur)
                return RouleurColor;
            else
                return SprinteurColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
