using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ImageViewer.Ui.Utilities.Events;
using Prism.Events;

namespace ImageViewer.ImageGridView.Views
{
    /// <summary>
    /// Interaction logic for ImageListView.xaml
    /// </summary>
    public partial class ImageListView : UserControl
    {
        public ImageListView()
        {
            InitializeComponent();
        }

        private void OnListBoxLoaded(object sender, EventArgs e)
        {
            ScrollViewerUtilities.BindScrollViewer(sender,e,ScrollPanel);
        }

        private void OnScrollViewerPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer)sender;
            if (e.Delta != 0)
            {
                scv.ScrollToVerticalOffset(scv.ContentVerticalOffset -
                                           e.Delta / Math.Abs(e.Delta) * 30);
            }
        }

        private static void OnScrollChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset < 500 && scrollViewer.ScrollableHeight != 0.0)
            {
                ((ImageGridViewModel)scrollViewer.DataContext).Refresh();

            }
        }
    }
}
