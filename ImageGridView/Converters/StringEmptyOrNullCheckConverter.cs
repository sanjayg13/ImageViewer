using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageViewer.ImageGridView.Converters
{
    /// <summary>
    /// Converter to check IsStringNullOrEmpty
    /// </summary>
    public class StringEmptyOrNullCheckConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new StringEmptyOrNullCheckConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.ToString().Equals(string.Empty))
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
