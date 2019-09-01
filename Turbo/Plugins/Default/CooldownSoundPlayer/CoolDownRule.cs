using System.Media;

namespace Turbo.Plugins.Default
{
    public class CoolDownRule
    {
        public ISnoPower SnoPower { get; }
        public SoundPlayer SoundPlayer { get; private set; }

        public CoolDownRule(ISnoPower snoPower, SoundPlayer soundPlayer)
        {
            SnoPower = snoPower;
            SoundPlayer = soundPlayer;
        }

        public void DisposeSoundPlayer()
        {
            SoundPlayer.Dispose();
            SoundPlayer = null;
        }
    }
}