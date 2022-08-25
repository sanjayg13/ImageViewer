using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer.Ui.Utilities.Interfaces
{
    /// <summary>
    /// Interface to retrieve the model of type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IModelProvider<T> where T : BaseModel
    {
        T GetModel(string url);
    }
}
