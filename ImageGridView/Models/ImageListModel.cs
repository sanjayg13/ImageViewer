using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ImageViewer.Ui.Utilities;
using ImageViewer.Ui.Utilities.Interfaces;

namespace ImageViewer.ImageGridView.Models
{
    /// <summary>
    ///  Image Model class contains the Images information.
    /// </summary>
    public class ImageListModel : BaseModel,IParsableResponse
    {
        public ObservableCollection<ImageModel> Images { get; set; } = new ObservableCollection<ImageModel>();

        public int Page { get; set; }

        public int PerPage { get; set; }

        public int Total { get; set; }

       

        /// <summary>
        ///     Parse xml content and update properties.
        /// </summary>
        /// <param name="xmlReader"></param>
        public void Parse(XmlReader xmlReader)
        {
            while (xmlReader.MoveToNextAttribute())
            {
                switch (xmlReader.LocalName)
                {
                    case "total":
                        Total = string.IsNullOrEmpty(xmlReader.Value) ? 0 : int.Parse(xmlReader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "perpage":
                        PerPage = string.IsNullOrEmpty(xmlReader.Value) ? 0 : int.Parse(xmlReader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "page":
                        Page = string.IsNullOrEmpty(xmlReader.Value) ? 0 : int.Parse(xmlReader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;

                    default:
                        break;

                }
            }

            xmlReader.Read();

            while (xmlReader.LocalName == "photo")
            {
                var p = new ImageModel();
                ((IParsableResponse)p).Parse(xmlReader);

                if (!string.IsNullOrEmpty(p.Id) || !string.IsNullOrEmpty(p.ServerId))
                {
                    Images.Add(p);
                }
            }

           
            xmlReader.Skip();
        }
    }
}
