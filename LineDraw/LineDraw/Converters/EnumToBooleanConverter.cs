using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace LineDraw.Converters
{
    /// <summary>
    /// This class provides conversion between  
    /// Enums and boolean.
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts submitted enum value to a boolean value. Returns true
        /// if the value is equal to parameter.
        /// </summary>
        /// <param name="value">Enum to convert</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Value to compare with</param>
        /// <param name="culture"></param>
        /// <returns>Boolean true if <paramref name="value"/> is equal 
        /// to <paramref name="parameter"/> else false</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        /// <summary>
        /// Converts submitted boolean to enum value. Returns <paramref name="parameter"/>
        /// if <paramref name="value"/> is true.
        /// </summary>
        /// <param name="value">Boolean to convert</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Value to return</param>
        /// <param name="culture"></param>
        /// <returns>If <paramref name="value"/> is true  
        /// returns <paramref name="parameter"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
