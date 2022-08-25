using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Unity;

namespace ImageViewer.Test.Utilities
{
    /// <summary>
    ///     TestBootStrapper.
    /// </summary>
    public class TestBootStrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new DependencyObject();
        }
    }
}
