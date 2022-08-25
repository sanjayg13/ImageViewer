using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer.ImageGridView
{
    /// <summary>
    ///     Utility class to construct the url with given parameters.
    /// </summary>
    public class UrlCreationUtility
    {
        public static string ConstructUrl(IReadOnlyDictionary<string, string> searchOptions)
        {
            StringBuilder Url = new StringBuilder();
            foreach (KeyValuePair<string, string> option in searchOptions)
            {
                Url.Append(option.Key + "=" + option.Value + "&");
            }

            return Url.ToString();
        }
    }
}
