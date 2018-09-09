using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface ITransparentCollection
    {
        IEnumerable<ITransparent> GetTransparents();
    }
}