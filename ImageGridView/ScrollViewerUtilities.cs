using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ImageViewer.ImageGridView
{
    /// <summary>
    ///     Scroll viewer Utilities
    /// </summary>
    public class ScrollViewerUtilities
    {
        public static ScrollViewer ImgScrollViewer;


        public static void BindScrollViewer(object sender, EventArgs e,ScrollViewer scrollViewer)
        {
            ImgScrollViewer = scrollViewer;
            var binding = new Binding("VerticalOffset") { Source = ImgScrollViewer };
            var offsetChangeListener = DependencyProperty.RegisterAttached(
                "ListenerOffset",
                typeof(object),
                typeof(UserControl),
                new PropertyMetadata(OnScrollChanged));
            scrollViewer.SetBinding(offsetChangeListener, binding);
        }

        private static void OnScrollChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset < 500 && scrollViewer.ScrollableHeight != 0.0)
            {
                ((ImageGridViewModel)scrollViewer.DataContext).Refresh();

            }
        }

        public static void ScrollToTop()
        {
            ImgScrollViewer.ScrollToTop();
        }



    }
}
