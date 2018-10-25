using FRCards.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace FRCards.Converters
{
    class RiderTypeToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RiderType rider = (RiderType)value;

            if (rider == RiderType.Rouleur)
                return new Thickness(6, 6, 4, 6);
            else
                return new Thickness(4, 6, 6, 6);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
