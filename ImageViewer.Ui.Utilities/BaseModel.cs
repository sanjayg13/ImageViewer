using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer.Ui.Utilities
{
    /// <summary>
    ///     Base class for all the models.
    /// </summary>
    public abstract class BaseModel
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        protected BaseModel()
        {
            
        }
    }
}
