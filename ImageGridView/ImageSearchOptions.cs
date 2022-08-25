using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer.ImageGridView
{
    /// <summary>
    ///     Search Options used to search.
    /// </summary>
    public class ImageSearchOptions
    {
        public string Page { get; set; }

        public string Text { get; set; }

        public string Method { get; set; }

        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public ImageSearchOptions(string text, string method= "flickr.photos.search", string page = "1")
        {
            Page = page;
            Text = text;
            Method = method;
        }

       
        public string GetSearchUrl()
        {
            if (!string.IsNullOrEmpty(Method))
            {
                AddToDictionary("method", Method);
            }

            if (!string.IsNullOrEmpty(Page))
            {
                AddToDictionary("page", Page);
            }

            if (!string.IsNullOrEmpty(Text))
            {
                AddToDictionary("text", Text);
            }
           
            var url = UrlCreationUtility.ConstructUrl(parameters);
            return url;
        }

        private void AddToDictionary(string key,string value)
        {
            if (parameters.ContainsKey(key))
            {
                parameters[key] = value;
            }
            else
            {
                parameters.Add(key, value);
            }
        }
    }
}
