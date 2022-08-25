using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageViewer.Ui.Utilities.Interfaces
{
    /// <summary>
    ///     Interface to Parse the Xml content. Models should implement this interface to
    ///     update the properties from API response.
    /// </summary>
    public interface IParsableResponse
    {
        void Parse(XmlReader xmlReader);
    }
}
