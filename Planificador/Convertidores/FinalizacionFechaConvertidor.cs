using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Planificador.Convertidores
{
    public class FinalizacionFechaConvertidor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fecha = (DateTime?)value;

            if (fecha != null)
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var estaHecha = (Boolean)value;

            if (estaHecha)
                return DateTime.Now;
            else
                return null;
        }
    }
}
