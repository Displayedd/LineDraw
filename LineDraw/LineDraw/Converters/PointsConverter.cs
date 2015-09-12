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
    /// <see cref="LineDraw.Models.Point"/> array
    /// and <see cref="System.Windows.Media.PointCollection"/>.
    /// </summary>
    public class PointsConverter : IValueConverter
    {
        /// <summary>
        /// Converts from an array of  <see cref="LineDraw.Models.Points"/>
        /// to <see cref="System.Windows.Media.PointCollection"/>.
        /// </summary>
        /// <param name="value">An array of <see cref="LineDraw.Models.Points"/></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>A <see cref="System.Windows.Media.PointCollection"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Point[] points = (Point[])value;
            PointCollection converted = new PointCollection();

            if (points != null)
                foreach (Point point in points)
                    converted.Add(new System.Windows.Point(point.X, point.Y));

            return converted;
        }

        /// <summary>
        /// Converts from an array of <see cref="System.Windows.Media.PointCollection"/>
        /// to <see cref="LineDraw.Models.Points"/>.
        /// </summary>
        /// <param name="value">A <see cref="System.Windows.Media.PointCollection"/></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>An array of <see cref="LineDraw.Models.Points"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PointCollection points = (PointCollection)value;
            List<Point> converted = new List<Point>();

            if (points != null)
            {
                foreach (System.Windows.Point point in points)
                    converted.Add(new Point { X = (int)point.X, Y = (int)point.Y });
            }

            return converted.ToArray();
        }
    }
}
