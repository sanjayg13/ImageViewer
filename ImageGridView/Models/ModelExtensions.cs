using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ImageViewer.Ui.Utilities.Interfaces;

namespace ImageViewer.ImageGridView.Models
{
    /// <summary>
    ///     Extension class for Flicker Api
    /// </summary>
    public  static class ModelExtensions
    {
        public static void Parse(this IParsableResponse item, string originalXml)
        {
            try
            {
                var reader = XmlReader.Create(new StringReader(originalXml), new XmlReaderSettings
                {
                    IgnoreWhitespace = true
                });

                if (!reader.ReadToDescendant("rsp"))
                {
                    throw new Exception("Unable to find response element 'rsp' in Flickr response");
                }
                while (reader.MoveToNextAttribute())
                {
                    if (reader.LocalName == "stat" && reader.Value == "fail")
                        throw new Exception("someting");
                }

                reader.MoveToElement();
                reader.Read();

                item.Parse(reader);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
