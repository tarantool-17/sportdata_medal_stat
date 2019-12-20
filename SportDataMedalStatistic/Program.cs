using System;
using System.Threading.Tasks;

namespace SportDataMedalStatistic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = new MedalStatisticService();
            var results = await service.GetMedalStatisticAsync();
            
            var i = 1;
            foreach(var res in results)
            {
                Console.WriteLine($"{i++}. {res}");
            }
        }
    }
}