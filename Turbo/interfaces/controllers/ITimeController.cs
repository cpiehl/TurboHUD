using System;

namespace Turbo.Plugins
{
    public interface ITimeController
    {
        DateTime Now { get; }

        IWatch CreateWatch();
        IWatch CreateAndStartWatch();
    }
}
