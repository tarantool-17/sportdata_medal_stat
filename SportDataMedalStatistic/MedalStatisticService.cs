using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportDataMedalStatistic
{
    public interface IMedalStatisticService
    {
        Task<List<CountryResult>> GetMedalStatisticAsync();
    }
    
    public class MedalStatisticService : IMedalStatisticService
    {
        private readonly IResultsRepository _repository;
        private readonly IPersonalResultParser _resultParser;

        public MedalStatisticService()
        {
            _repository = new ResultsRepository();
            _resultParser = new PersonalResultParser();
        }

        public async Task<List<CountryResult>> GetMedalStatisticAsync()
        {
            var doc = await _repository.GetHtmlResultsAsync();
            var results = _resultParser.Parse(doc);
            
            var result = results
                .GroupBy(x => new { x.Country, x.Rank })
                .Select(x => new {
                    Country = x.Key.Country,
                    Rank = x.Key.Rank,
                    Count = x.Count()
                })
                .GroupBy(x => x.Country)
                .Select(x => new CountryResult { 
                    Country = x.Key,
                    Results = x.ToDictionary(y => y.Rank, y => y.Count)
                })
                .ToList();
            
            result.Sort(CountryResult.Compare); 
            result.Reverse();

            return result;  
        }
    }
}