using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ImageViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ImageViewerBootStrapper _bootStrapper;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _bootStrapper = new ImageViewerBootStrapper();
            _bootStrapper.Run();
        }
    }
}
