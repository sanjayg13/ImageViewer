using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace ImageViewer.Ui.Utilities.Events
{
    /// <summary>
    ///     Event with Last searched keyword as Argument.
    /// </summary>
    public class InvokeLastSearchEvent : PubSubEvent<string>
    {
    }
}
