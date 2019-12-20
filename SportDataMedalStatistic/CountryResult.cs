using System;
using System.Collections.Generic;
using System.Linq;

namespace SportDataMedalStatistic
{
    public class CountryResult
    {
        public string Country { get; set; }
        public Dictionary<int, int> Results { get; set; }

        public static int Compare(CountryResult left, CountryResult right)
        { 
            if(left.Results.Count == 0 || right.Results.Count == 0)
                return left.Results.Count.CompareTo(right.Results.Count);

            var max = Math.Max(left.Results.Max(x => x.Key), right.Results.Max(x => x.Key));

            for(var i = 1; i < max; i++)
            {
                var containsLeft = left.Results.TryGetValue(i, out var leftValue);
                var containsRight = right.Results.TryGetValue(i, out var rightValue);

                if(!containsLeft && !containsRight)
                {
                    continue;
                } 

                if(leftValue == rightValue)
                {
                    continue;
                }

                return leftValue.CompareTo(rightValue);
            }

            return String.Compare(left.Country, right.Country, StringComparison.InvariantCultureIgnoreCase);        
        }

        public override string ToString()
        {
            return $"{Country} {string.Join(", ", Results.OrderBy(x => x.Key).Select(x => $"{x.Key} - {x.Value}"))}";
        }
    }
}