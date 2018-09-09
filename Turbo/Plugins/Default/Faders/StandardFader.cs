namespace Turbo.Plugins.Default
{

    public class StandardFader: IFader
    {

        public IController Hud { get; private set; }

        public float FadeOpacity { get; set; }
        public bool AllowFadeOut { get; set; }
        public bool AllowFadeIn { get; set; }
        public int FadeLength { get; set; }
        public int FadeOutLength { get; set; }

        private long _fadeDeadline = 0;
        private ITransparentCollection _collection;

        public StandardFader(IController hud, ITransparentCollection collection)
        {
            Hud = hud;
            _collection = collection;

            AllowFadeOut = true;
            AllowFadeIn = true;
            FadeLength = 130;
            FadeOutLength = 130;
            FadeOpacity = 0.0f;
        }

        public bool TestVisiblity(bool visibleTestResult)
        {
            var currentTicks = Hud.Game.CurrentRealTimeMilliseconds * 10000;

            if (!visibleTestResult)
            {
                if (FadeOpacity >= 1) _fadeDeadline = currentTicks + (FadeOutLength - 1) * 10000;
            }
            else
            {
                if (FadeOpacity <= 0) _fadeDeadline = currentTicks + (FadeLength - 1) * 10000;
            }

            if (!visibleTestResult)
            {
                if ((FadeOpacity <= 0) || !AllowFadeOut)
                {
                    FadeOpacity = 0;
                    return false;
                }
                FadeOpacity = ((float)((_fadeDeadline - currentTicks) / 10000.0f) / FadeOutLength);

                foreach (var transparentElement in _collection.GetTransparents()) transparentElement.Opacity = FadeOpacity;
                return true;
            }
            else
            {
                if ((FadeOpacity >= 1) && !AllowFadeIn)
                {
                    FadeOpacity = 1;
                    return true;
                }
                if (_fadeDeadline > currentTicks)
                {
                    FadeOpacity = 1 - ((float)((_fadeDeadline - currentTicks) / 10000.0f) / FadeLength);
                }
                else FadeOpacity = 1;
            }

            foreach (var transparentElement in _collection.GetTransparents()) transparentElement.Opacity = FadeOpacity;

            return visibleTestResult;
        }

    }

}