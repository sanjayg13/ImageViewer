using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageViewer.HeaderView.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace ImageViewer.HeaderView
{
    /// <summary>
    ///  Header view Module which hosts the Header view
    /// </summary>
    public class HeaderViewModule : IModule,IDisposable
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;
        private Header _headerView;

        public HeaderViewModule(IUnityContainer container, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _headerView = _container.Resolve<Header>();
            _container.RegisterInstance(_headerView, new ExternallyControlledLifetimeManager());
            var headerController = _container.Resolve<HeaderController>();
            _container.RegisterInstance(headerController, new ExternallyControlledLifetimeManager());
            _regionManager.RegisterViewWithRegion("HeaderViewRegion", () => _headerView);
        }

        public void Dispose()
        {
            if (_headerView != null)
            {
                if (_regionManager != null &&
                    _regionManager.Regions.ContainsRegionWithName("HeaderViewRegion"))
                {
                    _regionManager.Regions["HeaderViewRegion"].Deactivate(_headerView);
                    _regionManager.Regions["HeaderViewRegion"].Remove(_headerView);
                }
                _headerView = null;
            }
            DisposeContainer();
            _regionManager = null;
        }

        private void DisposeContainer()
        {
            if (_container != null)
            {
                _container.Dispose();
                _container = null;
            }
        }
    }
}
