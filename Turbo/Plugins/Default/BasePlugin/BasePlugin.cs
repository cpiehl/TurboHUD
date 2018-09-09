using System;
using System.Collections.Generic;
using System.Globalization;

namespace Turbo.Plugins.Default
{

    public abstract class BasePlugin: IPlugin
	{

        public IController Hud { get; private set; }
        public bool Enabled { get; set; }
        public int Order { get; set; }

        private Dictionary<string, IPerfCounter> _performanceCounters;
        public Dictionary<string, IPerfCounter> PerformanceCounters
        {
            get
            {
                if (_performanceCounters == null)
                {
                    _performanceCounters = new Dictionary<string, IPerfCounter>();
                }
                return _performanceCounters;
            }
        }

        public virtual void Load(IController hud)
        {
            Hud = hud;
        }

        public static string ValueToString(long Value, ValueFormat Format)
        {
            switch (Format)
            {
                case ValueFormat.NormalNumber:
                    return Value.ToString("#,0.#", CultureInfo.InvariantCulture);
                case ValueFormat.NormalNumberNoDecimal:
                    return Value.ToString("#,0", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysK:
                    return (Value / 1000.0f).ToString("#,0.#K", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysKNoDecimal:
                    return (Value / 1000.0f).ToString("#,0K", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysM:
                    return (Value / 1000.0f).ToString("#,0.#M", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysMNoDecimal:
                    return (Value / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture);
                case ValueFormat.LongNumber:
                    if (Value == 0) return "0";
                    if (Value < 100L * 1000L * 1000 * 1000 * 1000)
                    {
                        if (Value < 1000L * 1000 * 1000 * 1000)
                        {
                            if (Value < 100L * 1000 * 1000 * 1000)
                            {
                                if (Value < 1000 * 1000 * 1000)
                                {
                                    if (Value < 100 * 1000 * 1000)
                                    {
                                        if (Value < 1000 * 1000)
                                        {
                                            if (Value < 10 * 1000)
                                            {
                                                if (Value < 1 * 1000)
                                                {
                                                    if (Value < 1 * 100)
                                                    {
                                                        return Value.ToString("0.0", CultureInfo.InvariantCulture);
                                                    }
                                                    else return Value.ToString("0", CultureInfo.InvariantCulture);
                                                }
                                                else return Value.ToString("#,0", CultureInfo.InvariantCulture);
                                            }
                                            else return (Value / 1000.0f).ToString("#,0k", CultureInfo.InvariantCulture);
                                        }
                                        else return (Value / 1000.0f / 1000.0f).ToString("#,0.0M", CultureInfo.InvariantCulture);
                                    }
                                    else return (Value / 1000.0f / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture);
                                }
                                else return (Value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0bn", CultureInfo.InvariantCulture);
                            }
                            else return (Value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0bn", CultureInfo.InvariantCulture);
                        }
                        else return (Value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0tr", CultureInfo.InvariantCulture);
                    }
                    else return (Value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0tr", CultureInfo.InvariantCulture);
                case ValueFormat.ShortNumber:
                    if (Value == 0) return "0";
                    if (Value < 100L * 1000L * 1000 * 1000 * 1000)
                    {
                        if (Value < 1000L * 1000 * 1000 * 1000)
                        {
                            if (Value < 100L * 1000 * 1000 * 1000)
                            {
                                if (Value < 1000 * 1000 * 1000)
                                {
                                    if (Value < 100 * 1000 * 1000)
                                    {
                                        if (Value < 1000 * 1000)
                                        {
                                            if (Value < 1 * 1000)
                                            {
                                                if (Value < 1 * 100)
                                                {
                                                    return Value.ToString("0.0", CultureInfo.InvariantCulture); //1..99 = '55.0'
                                                }
                                                else return Value.ToString("0", CultureInfo.InvariantCulture); //100..999 = '555'
                                            }
                                            else return (Value / 1000.0f).ToString("#,0.#k", CultureInfo.InvariantCulture); // 10000... = 55.5K
                                        }
                                        else return (Value / 1000.0f / 1000.0f).ToString("#,0.0M", CultureInfo.InvariantCulture); // 1000000... = 1.0M
                                    }
                                    else return (Value / 1000.0f / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture); // 1000000000... = 1,000M
                                }
                                else return (Value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0bn", CultureInfo.InvariantCulture);
                            }
                            else return (Value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0bn", CultureInfo.InvariantCulture);
                        }
                        else return (Value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0tr", CultureInfo.InvariantCulture);
                    }
                    else return (Value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0tr", CultureInfo.InvariantCulture);
                case ValueFormat.LongTime:
                    {
                        long h = Value / (TimeSpan.TicksPerMillisecond * 60 * 60 * 1000); // value is in ticks
                        return (h > 0 ? h.ToString("D", CultureInfo.InvariantCulture) + "h " : "") + new TimeSpan(Value).ToString(@"m\m\ ss\s", CultureInfo.InvariantCulture);
                    }
                case ValueFormat.LongTimeNoSeconds:
                    {
                        long hrcount = Value / (TimeSpan.TicksPerMillisecond * 60 * 60 * 1000); // value is in ticks
                        return (hrcount > 0 ? hrcount.ToString("D", CultureInfo.InvariantCulture) + "h " : "") + new TimeSpan(Value).ToString(@"m\m", CultureInfo.InvariantCulture);
                    }
            }
            return null;
        }

        public static string ValueToString(double Value, ValueFormat Format)
        {
            switch (Format)
            {
                case ValueFormat.NormalNumber:
                    return Value.ToString("#,0.0", CultureInfo.InvariantCulture);
                case ValueFormat.NormalNumberNoDecimal:
                    return Math.Round(Value, 0).ToString("#,0", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysK:
                    return (Value / 1000.0f).ToString("#,0.0K", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysKNoDecimal:
                    return (Value / 1000.0f).ToString("#,0K", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysM:
                    return (Value / 1000.0f).ToString("#,0.0M", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysMNoDecimal:
                    return (Value / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture);
                case ValueFormat.LongNumber:
                    if (Value == 0) return "0";
                    if (Value < 100L * 1000L * 1000 * 1000 * 1000)
                    {
                        if (Value < 1000L * 1000 * 1000 * 1000)
                        {
                            if (Value < 100L * 1000 * 1000 * 1000)
                            {
                                if (Value < 1000 * 1000 * 1000)
                                {
                                    if (Value < 100 * 1000 * 1000)
                                    {
                                        if (Value < 1000 * 1000)
                                        {
                                            if (Value < 10 * 1000)
                                            {
                                                if (Value < 1 * 1000)
                                                {
                                                    if (Value < 1 * 100)
                                                    {
                                                        return Value.ToString("0.0", CultureInfo.InvariantCulture); //1..99 = '55.5'
                                                    }
                                                    else return Value.ToString("0", CultureInfo.InvariantCulture); //100..999 = '555'
                                                }
                                                else return Value.ToString("#,0", CultureInfo.InvariantCulture); // 1000..9999 = 5,555
                                            }
                                            else return (Value / 1000.0f).ToString("#,0k", CultureInfo.InvariantCulture); // 10000... = 55.5K
                                        }
                                        else return (Value / 1000.0f / 1000.0f).ToString("#,0.0M", CultureInfo.InvariantCulture); // 1000000... = 1.0M
                                    }
                                    else return (Value / 1000.0f / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture); // 1000000000... = 1,000M
                                }
                                else return (Value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0bn", CultureInfo.InvariantCulture);
                            }
                            else return (Value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0bn", CultureInfo.InvariantCulture);
                        }
                        else return (Value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0tr", CultureInfo.InvariantCulture);
                    }
                    else return (Value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0tr", CultureInfo.InvariantCulture);
                case ValueFormat.ShortNumber:
                    if (Value == 0) return "0";
                    if (Value < 100L * 1000L * 1000 * 1000 * 1000)
                    {
                        if (Value < 1000L * 1000 * 1000 * 1000)
                        {
                            if (Value < 100L * 1000 * 1000 * 1000)
                            {
                                if (Value < 1000 * 1000 * 1000)
                                {
                                    if (Value < 100 * 1000 * 1000)
                                    {
                                        if (Value < 1000 * 1000)
                                        {
                                            if (Value < 1 * 1000)
                                            {
                                                if (Value < 1 * 100)
                                                {
                                                    return Value.ToString("0.0", CultureInfo.InvariantCulture); //1..99 = '55.0'
                                                }
                                                else return Value.ToString("0", CultureInfo.InvariantCulture); //100..999 = '555'
                                            }
                                            else return (Value / 1000.0f).ToString("#,0.#k", CultureInfo.InvariantCulture); // 10000... = 55.5K
                                        }
                                        else return (Value / 1000.0f / 1000.0f).ToString("#,0.0M", CultureInfo.InvariantCulture); // 1000000... = 1.0M
                                    }
                                    else return (Value / 1000.0f / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture); // 1000000000... = 1,000M
                                }
                                else return (Value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0bn", CultureInfo.InvariantCulture);
                            }
                            else return (Value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0bn", CultureInfo.InvariantCulture);
                        }
                        else return (Value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0tr", CultureInfo.InvariantCulture);
                    }
                    else return (Value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0tr", CultureInfo.InvariantCulture);
            }
            return null;
        }

    }

}