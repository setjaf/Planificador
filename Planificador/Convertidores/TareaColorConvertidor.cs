using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Planificador.Convertidores
{
    public class TareaColorConvertidor : IValueConverter
    {
        public  object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (string)value;

            if(!String.IsNullOrEmpty(color) &&  color.Length == 6)
            {
                return Color.FromHex(("#"+color));
            }
            else
            {
                return Color.FromHex("#FFFFFF");
            }
        }

        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(
                $"{nameof(TareaColorConvertidor)} cannot be used on two-way bindings.");
        }
    }
}
