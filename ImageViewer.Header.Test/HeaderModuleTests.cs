using ImageViewer.HeaderView;
using ImageViewer.Test.Utilities;
using ImageViewer.Ui.Utilities.Events;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ImageViewer.Header.Test
{
    [Apartment(ApartmentState.STA)]
    public class TestHeaderModule
    {
        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;
        private HeaderViewModule _headerViewModule;

        /// <summary>
        ///     SetUp
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            TestHelper.SetupTestApplication();
            _container = new UnityContainer();

            var bootstrapper = new TestBootStrapper();
            bootstrapper.Run();

            SetupMocks();
            _headerViewModule = _container.Resolve<HeaderViewModule>();
            _headerViewModule.Initialize();
        }

        /// <summary>
        ///     TearDown
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            if (TestHelper.TestApplication.MainWindow != null)
            {
                TestHelper.TestApplication.MainWindow.Content = null;
            }

            _headerViewModule = null;
            _container.Dispose();
            _container = null;
        }

        /// <summary>
        ///     Test to check the header content.
        /// </summary>
        [Test]
        public void TestHeaderContent()
        {
             HeaderView.Views.Header header = _container.Resolve<HeaderView.Views.Header>();
            TestHelper.InitializeTestWindow(header);

            var searchButton = new List<Button>();
            TestHelper.FindVisualChild(header, "ImgSearchButton", searchButton);

            var undoButton = new List<Button>();
            TestHelper.FindVisualChild(header, "UndoButton", undoButton);

            var searchTextbox = new List<TextBox>();
            TestHelper.FindVisualChild(header, "ImgSearchTag", searchTextbox);

            Assert.AreEqual(Visibility.Visible,searchButton[0].Visibility);
            Assert.AreEqual(Visibility.Visible, undoButton[0].Visibility);
            Assert.AreEqual(Visibility.Visible, searchTextbox[0].Visibility);
        }

        /// <summary>
        ///     Test to check the header undo button click
        /// </summary>
        [Test]
        public void TestHeaderUndoButton()
        {
            HeaderView.Views.Header header = _container.Resolve<HeaderView.Views.Header>();
            TestHelper.InitializeTestWindow(header);

            var undoButton = new List<Button>();
            TestHelper.FindVisualChild(header, "UndoButton", undoButton);

            var searchButton = new List<Button>();
            TestHelper.FindVisualChild(header, "ImgSearchButton", searchButton);


            Assert.AreEqual(undoButton[0].IsEnabled,false);

            searchButton[0].Command.Execute(null);
            searchButton[0].Command.Execute(null);

            Assert.AreEqual(undoButton[0].IsEnabled, true);

            undoButton[0].Command.Execute(null);


            Assert.AreEqual(undoButton[0].IsEnabled, false);
        }

        /// <summary>
        ///     Test to check the header Search button click
        /// </summary>
        [Test]
        public void TestSearchButton()
        {
            HeaderView.Views.Header header = _container.Resolve<HeaderView.Views.Header>();
            TestHelper.InitializeTestWindow(header);

            var searchButton = new List<Button>();
            TestHelper.FindVisualChild(header, "ImgSearchButton", searchButton);

            bool isSearchInvoked = false;
            _eventAggregator.GetEvent<InvokeImageSearchEvent>().Subscribe((string s) => isSearchInvoked = true);

            searchButton[0].Command.Execute(null);


            Assert.AreEqual(isSearchInvoked, true);

        }

        private void SetupMocks()
        {
            _container.RegisterType<IRegionManager, RegionManager>();
            _container.RegisterType<IEventAggregator, EventAggregator>();
            var regionManager = _container.Resolve<IRegionManager>();
            _container.RegisterInstance(regionManager, new ContainerControlledLifetimeManager());
            _eventAggregator = _container.Resolve<IEventAggregator>();
            _container.RegisterInstance(_eventAggregator, new ContainerControlledLifetimeManager());
        }
    }
}
