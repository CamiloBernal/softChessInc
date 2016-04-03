using System;

namespace SoftChess.Inc.Data.Settings
{
    public class DbSettings<T>
        where T : class, IDbSettingSet
    {
        public DbSettings(T settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            Settings = settings;
        }

        public T Settings { get; }
    }
}