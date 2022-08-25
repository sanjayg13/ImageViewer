using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ImageViewer.ImageGridView.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using ImageViewer.Test.Utilities;
using Prism.Regions;
using Rhino.Mocks;
using ImageViewer.Ui.Utilities.Interfaces;
using FlickerComponent.Interfaces;
using ImageViewer.ImageGridView.Views;
using ImageViewer.Ui.Utilities.Events;

namespace ImageViewer.ImageGridView.Test
{
    [Apartment(ApartmentState.STA)]
    public class ImageGridModuleTests
    {
        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;
        private ImageGridViewModule _imageGridViewModule;

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
            _imageGridViewModule = _container.Resolve<ImageGridViewModule>();
            _imageGridViewModule.Initialize();
        }

        [Test]
        public void TestImageGridModule()
        {
            var imageGrid = _container.Resolve<ImageGrid>();
            TestHelper.InitializeTestWindow(imageGrid);

            var imageGridContentControl = new List<ContentControl>(); 
            TestHelper.FindVisualChild(imageGrid, "ImageGridViewContentControl", imageGridContentControl);

            var extendedImageControl = new List<ContentControl>();
            TestHelper.FindVisualChild(imageGrid, "ExtendedImageViewContentControl", extendedImageControl);

            _eventAggregator.GetEvent<InvokeImageSearchEvent>().Publish(null);

            var listBoxControl = new List<ItemsControl>();
            TestHelper.FindVisualChild(imageGrid, "ImageListBox", listBoxControl);

            listBoxControl[0]?.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();

            var listBoxItemGrid = new List<Grid>();
            TestHelper.FindVisualChild(imageGrid, "ImageView", listBoxItemGrid);
            TestHelper.TestApplication?.Dispatcher?.Invoke(() => { }, DispatcherPriority.Render);

            var selectedItemIndex = 0;
            RaiseMouseUpEvent(listBoxItemGrid[selectedItemIndex],UIElement.PreviewMouseDownEvent);


            Assert.AreEqual(imageGridContentControl[0].Visibility, Visibility.Visible);
            Assert.AreEqual(imageGridContentControl[0].Visibility, Visibility.Collapsed);
        }

        private void SetupMocks()
        {
            _container.RegisterType<IRegionManager, RegionManager>();
            _container.RegisterType<IEventAggregator, EventAggregator>();
             var regionManager = _container.Resolve<IRegionManager>();
            _container.RegisterInstance(regionManager, new ContainerControlledLifetimeManager());
            _eventAggregator = _container.Resolve<IEventAggregator>();
            _container.RegisterInstance(_eventAggregator, new ContainerControlledLifetimeManager());

            var imageListModelProvider = MockRepository.GenerateMock<IModelProvider<ImageListModel>>();

            var imageListModel = new ImageListModel();
            imageListModel.Images.Add(new ImageModel(){IsSelected = true});
            imageListModel.Images.Add(new ImageModel());
            
            imageListModelProvider
                .Stub(x => x.GetModel(Arg<string>.Is.Anything))
                .WhenCalled(_ =>
                {
                    imageListModelProvider.Stub(x => x.GetModel(Arg<string>.Is.Anything))
                        .Return( imageListModel);
                });

            _container.RegisterInstance(imageListModelProvider, new ContainerControlledLifetimeManager());
        }

        private static FrameworkElement RaiseMouseUpEvent(FrameworkElement frameworkElement, RoutedEvent routedEvent)
        {
            var eventArgs = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
            {
                RoutedEvent = routedEvent,
                Source = frameworkElement
            };
            frameworkElement.RaiseEvent(eventArgs);
            return frameworkElement;
        }
    }
}
