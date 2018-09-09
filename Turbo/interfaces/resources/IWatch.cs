namespace Turbo.Plugins
{
    public interface IWatch
    {
        bool TimerTest(int milliseconds);
        long ElapsedMilliseconds { get; }

        void Start();
        void Stop();
        void Reset();
        void Restart();
        bool IsRunning { get; }
    }
}