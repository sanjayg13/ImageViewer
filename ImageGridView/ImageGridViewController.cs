using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageViewer.ImageGridView.Views;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace ImageViewer.ImageGridView
{
    /// <summary>
    ///  ImageGridViewController which creates views and viewmodel.
    /// </summary>
    public class ImageGridViewController
    {
        public ImageGridViewController(IUnityContainer container)
        {
            var imageGridView = container.Resolve<ImageGrid>();
            var imageGridViewModel = container.Resolve<ImageGridViewModel>();
            imageGridView.DataContext = imageGridViewModel;
        }
    }
}
