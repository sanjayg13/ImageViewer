using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Media;
using System.Windows.Threading;

namespace ImageViewer.Test.Utilities
{
    public static class TestHelper
    {
        /// <summary>
        ///     TestApplication View property
        /// </summary>
        public static Application TestApplication { get; set; }

        /// <summary>
        ///     Initialize Test window
        /// </summary>
        public static Application SetupTestApplication()
        {
            if (TestApplication == null)
            {
                TestApplication = new Application
                {
                    MainWindow = new Window
                    {
                        WindowStyle = WindowStyle.None,
                        WindowState = WindowState.Normal,
                        Top = -3000,
                        Left = -3000
                    }
                };
                TestApplication.MainWindow?.Show();
                TestApplication.MainWindow?.Hide();
            }

            return TestApplication;
        }

        /// <summary>
        ///     Sets uiElement as content to TestApplication's MainWindow
        /// </summary>
        /// <param name="uiElement"></param>
        public static void InitializeTestWindow(UIElement uiElement)
        {
            if (TestApplication.MainWindow == null)
            {
                Assert.Fail("MainWindow is null");
            }

            TestApplication.MainWindow.Content = uiElement;
            TestApplication.MainWindow.Show();

            Refresh();
        }

        /// <summary>
        ///     Refresh application to drain out dispatch queue.
        /// </summary>
        public static void Refresh()
        {
            TestApplication.Dispatcher?.Invoke(() => { }, DispatcherPriority.Render);
        }

        public static void FindVisualChild<T>(DependencyObject depObj, string automationId, List<T> results)
            where T : DependencyObject
        {
            if (depObj == null)
            {
                return;
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T item)
                {
                    var name = item.GetValue(AutomationProperties.AutomationIdProperty) as string;
                    if (!string.IsNullOrEmpty(name) && string.Equals(name, automationId))
                    {
                        results.Add(item);
                    }
                }

                FindVisualChild(child, automationId, results);
            }
        }
    }
}
