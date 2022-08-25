using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer.Ui.Utilities.Events
{
    /// <summary>
    ///     Event with the Search tag
    /// </summary>
    public class InvokeImageSearchEvent : PubSubEvent<string>
    {
    }
}
