using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ImageViewer.Ui.Utilities;
using ImageViewer.Ui.Utilities.Interfaces;

namespace ImageViewer.ImageGridView.Models
{
    /// <summary>
    ///     Model class for Image
    /// </summary>
    public class ImageModel : BaseModel,IParsableResponse
    {
        public string ImageId { get; set; }

        public string ServerId { get; set; }

        public string Farm { get; set; }

        public string Secret { get; set; }

        public bool IsSelected { get; set; }

        public Uri Url => new Uri(string.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}.jpg", Farm, ServerId, ImageId, Secret));

        public ImageModel()
        {
            
        }

        /// <summary>
        ///     Parse the xml and Updates teh properties
        /// </summary>
        /// <param name="xmlReader"></param>
        public void Parse(XmlReader xmlReader)
        {
            while (xmlReader.MoveToNextAttribute())
            {
                switch (xmlReader.LocalName)
                {
                    case "id":
                        ImageId = xmlReader.Value;
                        if (string.IsNullOrEmpty(xmlReader.Value))
                        {
                            xmlReader.Skip();
                            return;
                        }
                        break;

                    case "secret":
                        Secret = xmlReader.Value;
                        break;
                    case "server":
                        ServerId = xmlReader.Value;
                        break;
                    case "farm":
                        Farm = xmlReader.Value;
                        break;

                    default:
                        break;
                }
            }

            xmlReader.Read();
        }
    }
}
