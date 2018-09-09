namespace Turbo.Plugins
{

    public interface IPlayerDamageInfo
    {

        double TotalDamage { get; set; }
        long RunDps { get; set; }

        long CurrentDps { get; set; }
        long MaximumDps { get; set; }
    }

}