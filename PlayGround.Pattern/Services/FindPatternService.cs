using System.Collections.Generic;
using System.Linq;
using PlayGround.Pattern.Models;

namespace PlayGround.Pattern.Services
{
    public class FindPatternService
    {
        public PatternResult[] FindPatterns(string inputStr, int patternLength, bool allowOverLapping = false)
        {
            var map = new Dictionary<string, PatternResult>();
            for (var i = 0; i <= inputStr.Length - patternLength; i++)
            {
                var current = inputStr.Substring(i, patternLength);
                if (map.ContainsKey(current))
                {
                    map[current].Occurrence++;
                    if (allowOverLapping) continue;

                    i += patternLength - 1;
                    //remove these that have overlapping
                    map = map.Where(p => p.Value.Index <= map[current].Index - patternLength || p.Value.Index == map[current].Index || p.Value.Index >= map[current].Index + patternLength)
                        .ToDictionary(x => x.Key, x => x.Value);
                }
                else
                {
                    map.Add(current, new PatternResult() { Pattern = current, Occurrence = 1, Index = i });
                }
            }

            var result = map.Where(p => p.Value.Occurrence > 1).Select(p => p.Value)
                .ToArray();

            return result;
        }
    }


}
