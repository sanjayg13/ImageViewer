using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FlickerComponent;
using FlickerComponent.Interfaces;
using ImageViewer.ImageGridView.Models;
using ImageViewer.ImageGridView.Views;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;


namespace ImageViewer.ImageGridView
{
    /// <summary>
    ///     Module hosts the Image Grid view.
    /// </summary>
    public class ImageGridViewModule : IModule,IDisposable
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;
        private ImageGrid _imageGridView;

        public void Initialize()
        {
           
            _imageGridView = _container.Resolve<ImageGrid>();
            _container.RegisterInstance(_imageGridView, new ExternallyControlledLifetimeManager());
            var imageGridViewController = _container.Resolve<ImageGridViewController>();
            _container.RegisterInstance(imageGridViewController, new ExternallyControlledLifetimeManager());
            _regionManager.RegisterViewWithRegion("ImageGridViewRegion", () => _imageGridView);
        }

        public void Dispose()
        {
            if (_imageGridView != null)
            {
                if (_regionManager != null &&
                    _regionManager.Regions.ContainsRegionWithName("ImageGridViewRegion"))
                {
                    // Remove all Views
                    _regionManager.Regions["ImageGridViewRegion"].Deactivate(_imageGridView);
                    _regionManager.Regions["ImageGridViewRegion"].Remove(_imageGridView);
                }
                _imageGridView = null;
            }
            DisposeContainer();
            _regionManager = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="regionManager"></param>
        public ImageGridViewModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            LoadConfiguration(_container);
            _regionManager = regionManager;
        }

        private void DisposeContainer()
        {
            if (_container != null)
            {
                _container.Dispose();
                _container = null;
            }
        }

        private void LoadConfiguration(IUnityContainer container)
        {
            var configuration =
                ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            var unityConfigurationSection = (UnityConfigurationSection)configuration.GetSection("unity");
            _container = container;
            _container.RegisterType<IResponseHandler, FlickerApiResponseHandler>();
            _container.LoadConfiguration(unityConfigurationSection);
        }
    }
}
