using ImageViewer.Ui.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageViewer.ImageGridView.Models;
using ImageViewer.Ui.Utilities.Events;
using ImageViewer.Ui.Utilities.Interfaces;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace ImageViewer.ImageGridView
{
    /// <summary>
    ///     ViewModel for ImageGridView
    /// </summary>
    public class ImageGridViewModel : Observable,IDisposable
    {
        public const int RefreshCount = 3;
        private readonly Dictionary<string, ImageListModel> _priorSearchesDictionary = new Dictionary<string, ImageListModel>();

        private IEventAggregator _eventAggregator;
        private IModelProvider<ImageListModel> _imageModelProvider;
        private ImageListModel _imageListModel = new ImageListModel();
        private ImageModel _selectedImageModel;
        private int _selectedImageIndex;
        private bool _disposed;

        public ImageSearchOptions ImageSearchOptions { get; set; }

        public ImageListModel ImageListModel
        {
            get => _imageListModel;
            set => SetField(ref _imageListModel, value);
        }
        
        public ImageModel SelectedImageModel
        {
            get => _selectedImageModel;
            set => SetField(ref _selectedImageModel, value);
        }
        
        public int SelectedImageIndex
        {
            get => _selectedImageIndex;
            set => SetField(ref _selectedImageIndex
                , value);
        }

        public ImageGridViewModel(IUnityContainer container,IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _imageModelProvider = container.Resolve<IModelProvider<ImageListModel>>();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _eventAggregator.GetEvent<InvokeImageSearchEvent>().Subscribe(OnSearchClickInvoked);
            _eventAggregator.GetEvent<InvokeLastSearchEvent>().Subscribe(OnLastSearchInvoked);
        }

        private void OnSearchClickInvoked(string keyword)
        {
            ImageSearchOptions = new ImageSearchOptions(keyword);
            if (_imageModelProvider != null)
            {
                ImageListModel = _imageModelProvider.GetModel(ImageSearchOptions.GetSearchUrl());
            }

            if (keyword != null && !_priorSearchesDictionary.ContainsKey(keyword))
            {
                _priorSearchesDictionary.Add(keyword, ImageListModel);
            }

            SelectedImageIndex = -1;
            ScrollViewerUtilities.ScrollToTop();
        }

        private void OnLastSearchInvoked(string keyword)
        {
            if (_priorSearchesDictionary.ContainsKey(keyword))
            {
                ImageListModel = _priorSearchesDictionary[keyword];
                _priorSearchesDictionary.Remove(keyword);
            }
        }

        public void Refresh()
        {
            var currentPage = Convert.ToInt32(ImageSearchOptions.Page);
            currentPage++;

            if (currentPage < RefreshCount)
            {
                ImageSearchOptions.Page = currentPage.ToString();
                if (_imageModelProvider != null)
                {
                    var refreshedImageListModel = _imageModelProvider?.GetModel(ImageSearchOptions.GetSearchUrl());
                    ImageListModel.Images.AddRange(refreshedImageListModel?.Images);
                }


                if (_priorSearchesDictionary.ContainsKey(ImageSearchOptions.Text))
                {
                    _priorSearchesDictionary[ImageSearchOptions.Text] = ImageListModel;
                }
            }
        }

        private void UnSubscribeToEvents()
        {
            _eventAggregator.GetEvent<InvokeImageSearchEvent>().Unsubscribe(OnSearchClickInvoked);
            _eventAggregator.GetEvent<InvokeLastSearchEvent>().Unsubscribe(OnLastSearchInvoked);
        }

        /// <summary>
        ///     Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

      
        /// <summary>
        ///     Dispose pattern
        /// </summary>
        /// <param name="disposing">
        ///     Information if the object is being disposed by GC or user triggered.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    UnSubscribeToEvents();
                    _imageModelProvider = null;
                    _imageListModel = null;
                    _eventAggregator = null;
                    _disposed = true;
                }
            }
        }
    }
}
