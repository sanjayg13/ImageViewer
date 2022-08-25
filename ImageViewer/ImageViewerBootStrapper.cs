using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ImageViewer.HeaderView;
using ImageViewer.ImageGridView;
using Prism.Modularity;
using Prism.Unity;

namespace ImageViewer
{
    /// <summary>
    ///  Boot strapper class to configure container and initialize components.
    /// </summary>
    public class ImageViewerBootStrapper : UnityBootstrapper
    {
        #region Overridden Methods      
        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
        }
            
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<Shell>();
        }

        protected override void InitializeShell()
        {
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }


        /// <summary>
        ///     CreateModule Catalog
        /// </summary>
        /// <returns></returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = new ConfigurationModuleCatalog();
            return moduleCatalog;
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(HeaderViewModule));
            moduleCatalog.AddModule(typeof(ImageGridViewModule));

        }
        #endregion
    }
}
