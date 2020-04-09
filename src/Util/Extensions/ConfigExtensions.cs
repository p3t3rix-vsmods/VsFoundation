using System;
using Vintagestory.API.Common;

namespace Foundation.Util.Extensions
{
    public static class ConfigExtensions
    {
        public static TConfig LoadOrCreateConfig<TConfig>(this ICoreAPI api, string filename) where TConfig : new()
        {
            try
            {
                var loadedConfig = api.LoadModConfig<TConfig>(filename);
                if (loadedConfig != null)
                {
                    return loadedConfig;
                }
                return InitializeNewConfig<TConfig>(api, filename);
            }
            catch (Exception e)
            {
                api.World.Logger.Error($"Failed loading {filename}, error {e}. Will initialize new one");
                return InitializeNewConfig<TConfig>(api, filename);
            }
        }

        private static TConfig InitializeNewConfig<TConfig>(ICoreAPI api, string filename) where TConfig : new()
        {
            var config = new TConfig();
            api.StoreModConfig(config, filename);
            return config;
        }
    }
}
