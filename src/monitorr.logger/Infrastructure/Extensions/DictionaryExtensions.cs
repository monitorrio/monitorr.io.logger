using System.Collections.Generic;
using System.Linq;

namespace monitorr.logger.Infrastructure.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, string> RemoveInvalidCharacters(this Dictionary<string, string> dict)
        {
            if (dict == null)
            {
                return null;
            }

            var invalidCharacters = new[]
          {
                ".",
                "$"
            };
            var replacementChar = "_";

            return dict.Select(x =>
                   {
                       var key = x.Key;
                       foreach (var invalidCharacter in invalidCharacters)
                       {
                           key = key.Replace(invalidCharacter, replacementChar);
                       }
                       return new KeyValuePair<string, string>(key, x.Value);
                   })
                   .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
