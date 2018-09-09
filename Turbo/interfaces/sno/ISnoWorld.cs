using System.Collections.Generic;

namespace Turbo.Plugins
{
    public interface ISnoWorld
    {
        uint Sno { get; }
        List<ISnoArea> SnoAreas { get; }
    }
}