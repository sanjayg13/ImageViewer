using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageViewer.ImageGridView.Views
{
    /// <summary>
    /// Interaction logic for ImageGrid.xaml
    /// </summary>
    public partial class ImageGrid : UserControl
    {
        public ImageGrid()
        {
            ApplyTheme("/ImageViewer.ImageGridView;component/Themes/ImageGridViewTheme.xaml");
            InitializeComponent();
        }

        private static void ApplyTheme(string theme)
        {
            var themeUri = new Uri(theme,
                UriKind.RelativeOrAbsolute);
            if (Application.Current.Resources.MergedDictionaries.All(x => x.Source != themeUri))
            {
                var themeDictionary = new ResourceDictionary { Source = themeUri };
                Application.Current.Resources.MergedDictionaries.Add(themeDictionary);
            }
        }
    }
}
