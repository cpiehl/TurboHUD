using System.Windows.Forms;

namespace Turbo.Plugins
{
    public interface IInputController
    {
        bool IsKeyDown(Keys key);
        IKeyEvent CreateKeyEvent(bool isPressed, SharpDX.DirectInput.Key key, bool controlPressed, bool altPressed, bool shiftPressed);
    }
}