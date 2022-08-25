using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageViewer.HeaderView.Views;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace ImageViewer.HeaderView
{
    /// <summary>
    ///     Header Controller that binds the view and viewmodel.
    /// </summary>
    public class HeaderController
    {
        public HeaderController(IUnityContainer container,IEventAggregator eventAggregator)
        {
            var headerView = container.Resolve<Header>();
            var headerViewModel = container.Resolve<HeaderViewModel>();
            headerView.DataContext = headerViewModel;
        }
    }
}
