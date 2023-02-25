using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfmlAppLib
{
    public abstract class AbstractEventHandlerFac
    {
        public abstract ICollection<EventDrawable> CreateInteractiveObjects();
    }
}
