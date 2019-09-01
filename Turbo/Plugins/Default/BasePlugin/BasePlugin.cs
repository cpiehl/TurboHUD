using System;
using System.Collections.Generic;
using System.Globalization;

namespace Turbo.Plugins.Default
{
    public abstract class BasePlugin : IPlugin
    {
        public IController Hud { get; private set; }
        public bool Enabled { get; set; }
        public int Order { get; set; }

        private Dictionary<string, IPerfCounter> _performanceCounters;

        public Dictionary<string, IPerfCounter> PerformanceCounters => _performanceCounters ?? (_performanceCounters = new Dictionary<string, IPerfCounter>());

        public virtual void Load(IController hud)
        {
            Hud = hud;
        }

        public static string ValueToString(long value, ValueFormat format)
        {
            if (value < 0)
                return "-" + ValueToString(-value, format);

            switch (format)
            {
                case ValueFormat.NormalNumber:
                    return value.ToString("#,0.#", CultureInfo.InvariantCulture);
                case ValueFormat.NormalNumberNoDecimal:
                    return value.ToString("#,0", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysK:
                    return (value / 1000.0f).ToString("#,0.#K", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysKNoDecimal:
                    return (value / 1000.0f).ToString("#,0K", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysM:
                    return (value / 1000.0f).ToString("#,0.#M", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysMNoDecimal:
                    return (value / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture);
                case ValueFormat.LongNumber:
                    if (value == 0)
                        return "0";
                    if (value < 100L * 1000 * 1000 * 1000 * 1000 * 1000)
                    {
                        if (value < 1000L * 1000 * 1000 * 1000 * 1000)
                        {
                            if (value < 100L * 1000 * 1000 * 1000 * 1000)
                            {
                                if (value < 1000L * 1000 * 1000 * 1000)
                                {
                                    if (value < 100L * 1000 * 1000 * 1000)
                                    {
                                        if (value < 1000 * 1000 * 1000)
                                        {
                                            if (value < 100 * 1000 * 1000)
                                            {
                                                if (value < 1000 * 1000)
                                                {
                                                    if (value < 10 * 1000)
                                                    {
                                                        if (value < 1 * 1000)
                                                        {
#pragma warning disable IDE0046 // Convert to conditional expression
                                                            if (value < 1 * 100)
#pragma warning restore IDE0046 // Convert to conditional expression
                                                            {
                                                                return value.ToString("0.0", CultureInfo.InvariantCulture);
                                                            }

                                                            return value.ToString("0", CultureInfo.InvariantCulture);
                                                        }

                                                        return value.ToString("#,0", CultureInfo.InvariantCulture);
                                                    }

                                                    return (value / 1000.0f).ToString("#,0k", CultureInfo.InvariantCulture);
                                                }

                                                return (value / 1000.0f / 1000.0f).ToString("#,0.0M", CultureInfo.InvariantCulture);
                                            }

                                            return (value / 1000.0f / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture);
                                        }

                                        return (value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0bn", CultureInfo.InvariantCulture);
                                    }

                                    return (value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0bn", CultureInfo.InvariantCulture);
                                }

                                return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0tr", CultureInfo.InvariantCulture);
                            }

                            return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0tr", CultureInfo.InvariantCulture);
                        }

                        return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0qd", CultureInfo.InvariantCulture);
                    }

                    return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0qd", CultureInfo.InvariantCulture);
                case ValueFormat.ShortNumber:
                    if (value == 0)
                        return "0";
                    if (value < 100L * 1000 * 1000 * 1000 * 1000 * 1000)
                    {
                        if (value < 1000L * 1000 * 1000 * 1000 * 1000)
                        {
                            if (value < 100L * 1000L * 1000 * 1000 * 1000)
                            {
                                if (value < 1000L * 1000 * 1000 * 1000)
                                {
                                    if (value < 100L * 1000 * 1000 * 1000)
                                    {
                                        if (value < 1000 * 1000 * 1000)
                                        {
                                            if (value < 100 * 1000 * 1000)
                                            {
                                                if (value < 1000 * 1000)
                                                {
                                                    if (value < 1 * 1000)
                                                    {
                                                        if (value < 1 * 100)
                                                        {
                                                            return value.ToString("0.0", CultureInfo.InvariantCulture); //1..99 = '55.0'
                                                        }

                                                        return value.ToString("0", CultureInfo.InvariantCulture); //100..999 = '555'
                                                    }

                                                    return (value / 1000.0f).ToString("#,0.#k", CultureInfo.InvariantCulture); // 10000... = 55.5K
                                                }

                                                return (value / 1000.0f / 1000.0f).ToString("#,0.0M", CultureInfo.InvariantCulture); // 1000000... = 1.0M
                                            }

                                            return (value / 1000.0f / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture); // 1000000000... = 1,000M
                                        }

                                        return (value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0bn", CultureInfo.InvariantCulture);
                                    }

                                    return (value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0bn", CultureInfo.InvariantCulture);
                                }

                                return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0tr", CultureInfo.InvariantCulture);
                            }

                            return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0tr", CultureInfo.InvariantCulture);
                        }

                        return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0qd", CultureInfo.InvariantCulture);
                    }

                    return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0qd", CultureInfo.InvariantCulture);

                case ValueFormat.LongTime:
                    {
                        var h = value / (TimeSpan.TicksPerMillisecond * 60 * 60 * 1000); // value is in ticks
                        return (h > 0 ? h.ToString("D", CultureInfo.InvariantCulture) + "h " : "") + new TimeSpan(value).ToString(@"m\m\ ss\s", CultureInfo.InvariantCulture);
                    }
                case ValueFormat.LongTimeNoSeconds:
                    {
                        var hrcount = value / (TimeSpan.TicksPerMillisecond * 60 * 60 * 1000); // value is in ticks
                        return (hrcount > 0 ? hrcount.ToString("D", CultureInfo.InvariantCulture) + "h " : "") + new TimeSpan(value).ToString(@"m\m", CultureInfo.InvariantCulture);
                    }
            }

            return null;
        }

        public static string ValueToString(double value, ValueFormat format)
        {
            if (value < 0)
                return "-" + ValueToString(-value, format);

            switch (format)
            {
                case ValueFormat.NormalNumber:
                    return value.ToString("#,0.0", CultureInfo.InvariantCulture);
                case ValueFormat.NormalNumberNoDecimal:
                    return Math.Round(value, 0).ToString("#,0", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysK:
                    return (value / 1000.0f).ToString("#,0.0K", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysKNoDecimal:
                    return (value / 1000.0f).ToString("#,0K", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysM:
                    return (value / 1000.0f).ToString("#,0.0M", CultureInfo.InvariantCulture);
                case ValueFormat.AlwaysMNoDecimal:
                    return (value / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture);
                case ValueFormat.LongNumber:
                    if (value == 0)
                        return "0";
                    if (value < 100L * 1000 * 1000 * 1000 * 1000 * 1000)
                    {
                        if (value < 1000L * 1000 * 1000 * 1000 * 1000)
                        {
                            if (value < 100L * 1000 * 1000 * 1000 * 1000)
                            {
                                if (value < 1000L * 1000 * 1000 * 1000)
                                {
                                    if (value < 100L * 1000 * 1000 * 1000)
                                    {
                                        if (value < 1000 * 1000 * 1000)
                                        {
                                            if (value < 100 * 1000 * 1000)
                                            {
                                                if (value < 1000 * 1000)
                                                {
                                                    if (value < 10 * 1000)
                                                    {
                                                        if (value < 1 * 1000)
                                                        {
#pragma warning disable IDE0046 // Convert to conditional expression
                                                            if (value < 1 * 100)
#pragma warning restore IDE0046 // Convert to conditional expression
                                                            {
                                                                return value.ToString("0.0", CultureInfo.InvariantCulture);
                                                            }

                                                            return value.ToString("0", CultureInfo.InvariantCulture);
                                                        }

                                                        return value.ToString("#,0", CultureInfo.InvariantCulture);
                                                    }

                                                    return (value / 1000.0f).ToString("#,0k", CultureInfo.InvariantCulture);
                                                }

                                                return (value / 1000.0f / 1000.0f).ToString("#,0.0M", CultureInfo.InvariantCulture);
                                            }

                                            return (value / 1000.0f / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture);
                                        }

                                        return (value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0bn", CultureInfo.InvariantCulture);
                                    }

                                    return (value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0bn", CultureInfo.InvariantCulture);
                                }

                                return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0tr", CultureInfo.InvariantCulture);
                            }

                            return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0tr", CultureInfo.InvariantCulture);
                        }

                        return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0qd", CultureInfo.InvariantCulture);
                    }

                    return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0qd", CultureInfo.InvariantCulture);
                case ValueFormat.ShortNumber:
                    if (value == 0)
                        return "0";
                    if (value < 100L * 1000 * 1000 * 1000 * 1000 * 1000)
                    {
                        if (value < 1000L * 1000 * 1000 * 1000 * 1000)
                        {
                            if (value < 100L * 1000L * 1000 * 1000 * 1000)
                            {
                                if (value < 1000L * 1000 * 1000 * 1000)
                                {
                                    if (value < 100L * 1000 * 1000 * 1000)
                                    {
                                        if (value < 1000 * 1000 * 1000)
                                        {
                                            if (value < 100 * 1000 * 1000)
                                            {
                                                if (value < 1000 * 1000)
                                                {
                                                    if (value < 1 * 1000)
                                                    {
                                                        if (value < 1 * 100)
                                                        {
                                                            return value.ToString("0.0", CultureInfo.InvariantCulture); //1..99 = '55.0'
                                                        }

                                                        return value.ToString("0", CultureInfo.InvariantCulture); //100..999 = '555'
                                                    }

                                                    return (value / 1000.0f).ToString("#,0.#k", CultureInfo.InvariantCulture); // 10000... = 55.5K
                                                }

                                                return (value / 1000.0f / 1000.0f).ToString("#,0.0M", CultureInfo.InvariantCulture); // 1000000... = 1.0M
                                            }

                                            return (value / 1000.0f / 1000.0f).ToString("#,0M", CultureInfo.InvariantCulture); // 1000000000... = 1,000M
                                        }

                                        return (value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0bn", CultureInfo.InvariantCulture);
                                    }

                                    return (value / 1000.0f / 1000.0f / 1000.0f).ToString("#,0bn", CultureInfo.InvariantCulture);
                                }

                                return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0tr", CultureInfo.InvariantCulture);
                            }

                            return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0tr", CultureInfo.InvariantCulture);
                        }

                        return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0.0qd", CultureInfo.InvariantCulture);
                    }

                    return (value / 1000.0f / 1000.0f / 1000.0f / 1000.0f / 1000.0f).ToString("#,0qd", CultureInfo.InvariantCulture);
            }

            return null;
        }
    }
}