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
    /// Interaction logic for ExtendedImageView.xaml
    /// </summary>
    public partial class ExtendedImageView : UserControl
    {
        private bool _zoomIn = false;
        public ExtendedImageView()
        {
            InitializeComponent();
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            ((ImageGridViewModel)DataContext).SelectedImageIndex = -1;
        }

        private void ExpandedImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _zoomIn = !_zoomIn;
            if (_zoomIn)
            {
                ExpandedImage.Stretch = Stretch.Fill;
                BackToGridViewButton.Visibility = Visibility.Hidden;

            }
            else
            {
                ExpandedImage.Stretch = Stretch.None;
                BackToGridViewButton.Visibility = Visibility.Visible;
            }
        }
    }
}
