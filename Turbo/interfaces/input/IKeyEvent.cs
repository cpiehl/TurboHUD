using SharpDX.DirectInput;

namespace Turbo.Plugins
{
    public interface IKeyEvent
    {
        bool IsPressed { get; }
        Key Key { get; }

        bool ControlPressed { get; }
        bool AltPressed { get; }
        bool ShiftPressed { get; }

        bool Matches(IKeyEvent otherKeyEvent);
    }
}