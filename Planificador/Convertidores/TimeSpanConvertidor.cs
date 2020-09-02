using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Planificador.Convertidores
{
    public class TimeSpanConvertidor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hora = (TimeSpan)value;

            return hora.ToString(@"hh\:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(
                $"{nameof(TareaColorConvertidor)} cannot be used on two-way bindings.");
        }
    }
}
