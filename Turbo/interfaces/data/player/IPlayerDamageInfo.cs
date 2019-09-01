namespace Turbo.Plugins
{
    public interface IPlayerDamageInfo
    {
        double TotalDamage { get; set; }
        double RunDps { get; set; }

        double CurrentDps { get; set; }
        double MaximumDps { get; set; }
    }
}