using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ImageViewer.ImageGridView.Converters
{
    /// <summary>
    ///  Converter to Enable/Disable Undo Button.
    /// </summary>
    public class SelectionToBooleanConverter : IValueConverter
    {
        public static SelectionToBooleanConverter Instance { get; } = new SelectionToBooleanConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
