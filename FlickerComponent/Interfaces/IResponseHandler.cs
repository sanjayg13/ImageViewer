using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickerComponent.Interfaces
{
    /// <summary>
    ///  Interface 
    /// </summary>
    public interface IResponseHandler
    {
        string GetResponse(string url);
    }
}
