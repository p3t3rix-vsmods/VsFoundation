using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vintagestory.API.Config;

namespace Foundation.Utils
{
    public static class LangUtils
    {
        public static string[] GetAllLanguageStringsOfKey(string key)
        {
            var finalList = new List<string>();

            var path = Path.Combine(GamePaths.AssetsPath, "game/lang/");
            foreach (var fileName in Directory.EnumerateFiles(path))
            {
                if (Path.GetFileName(fileName) == "languages.json")
                    continue;

                var rawFileContents = File.ReadAllText(Path.Combine(path, fileName));
                var jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawFileContents);
                if (jsonData.TryGetValue(key, out var value))
                    finalList.Add(value);
            }

            return finalList.ToArray();
        }
    }
}
