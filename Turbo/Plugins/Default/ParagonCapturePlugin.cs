using System;
using System.Globalization;

namespace Turbo.Plugins.Default
{
    public class ParagonCapturePlugin : BasePlugin, INewAreaHandler, IBeforeRenderHandler
    {
        public bool StopRenderingWhenCapturing { get; set; }
        public string SubFolderName { get; set; }
        public int DelayBetweenFrames { get; set; }

        private IWatch _lastNewGame;
        private IWatch _lastLevelUp, _lastLevelUpDelay, _lastLevelUpLimiter;

        public ParagonCapturePlugin()
        {
            Enabled = true;
            StopRenderingWhenCapturing = true;
            SubFolderName = "capture_paragon";
            DelayBetweenFrames = 200;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);

            _lastNewGame = Hud.Time.CreateWatch();
            _lastLevelUp = Hud.Time.CreateWatch();
            _lastLevelUpDelay = null;
            _lastLevelUpLimiter = Hud.Time.CreateWatch();
        }

        public void OnNewArea(bool newGame, ISnoArea area)
        {
            if (newGame)
            {
                _lastNewGame.Restart();
            }
        }

        public void BeforeRender()
        {
            if (!Enabled) return;
            if (!Hud.Game.IsInGame) return;

            var captureOn = Hud.Render.ParagonLevelUpSplashTextUiElement.Visible
                && !string.IsNullOrEmpty(Hud.MyBattleTag)
                && _lastNewGame.TimerTest(10 * 1000);

            if (!captureOn)
            {
                if (StopRenderingWhenCapturing && !Hud.Render.IsRenderEnabled)
                {
                    // turn back
                    Hud.Render.IsRenderEnabled = true;
                }
                return;
            }

            if (StopRenderingWhenCapturing && Hud.Render.IsRenderEnabled)
            {
                // turn off and wait a cycle before first capture so HUD can disappear properly
                Hud.Render.IsRenderEnabled = false;
                return;
            }

            var paragonLevel = Hud.Game.Me.CurrentLevelParagon;
            if (_lastLevelUp.TimerTest(120 * 1000))
            {
                _lastLevelUp.Restart();
                _lastLevelUpDelay = null;
                _lastLevelUpLimiter.Restart();

                Hud.TextLog.Log("levelup_paragon_" + Hud.MyBattleTag, paragonLevel.ToString("D", CultureInfo.InvariantCulture));
            }
            if ((_lastLevelUpDelay == null || _lastLevelUpDelay.TimerTest(DelayBetweenFrames)) && !_lastLevelUpLimiter.TimerTest(8 * 1000))
            {
                if (_lastLevelUpDelay == null)
                {
                    _lastLevelUpDelay = Hud.Time.CreateWatch();
                }

                _lastLevelUpDelay.Restart();
                try
                {
                    var fileName = Hud.MyBattleTag + "_" + Hud.Game.Me.HeroId.ToString("D", CultureInfo.InvariantCulture) + "_" + Hud.Game.Me.HeroName + "_" + Hud.Time.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture) + "_" + paragonLevel.ToString("D", CultureInfo.InvariantCulture) + ".jpg";
                    Hud.Render.CaptureScreenToFile(SubFolderName, fileName);
                }
                catch (Exception) { }
                finally { }
            }
        }
    }
}