using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FlickerComponent.Interfaces;
using ImageViewer.ImageGridView.Models;
using ImageViewer.Ui.Utilities.Interfaces;
using Microsoft.Practices.Unity;

namespace ImageViewer.ImageGridView
{
    /// <summary>
    ///     Model Provider to provide the Images
    /// </summary>
    public class ImageModelProvider : IModelProvider<ImageListModel>
    {
        private readonly IResponseHandler _flickarResponseHandler;
        private IUnityContainer _container;
        private ImageListModel _imageListModel;
        
        public ImageModelProvider(IUnityContainer container)
        {
            _container = container;
            _flickarResponseHandler = _container.Resolve<IResponseHandler>();
        }

        public ImageListModel GetModel(string imgSearchUrl)
        {
            _imageListModel = new ImageListModel();
            try
            {
                var response = _flickarResponseHandler.GetResponse(imgSearchUrl);
                _imageListModel.Parse(response);
                return _imageListModel;
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
                return _imageListModel;
            }
        }
    }
}
