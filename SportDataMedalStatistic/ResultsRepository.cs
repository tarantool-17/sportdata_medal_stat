using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SportDataMedalStatistic
{
    public interface IResultsRepository
    {
        Task<HtmlDocument> GetHtmlResultsAsync();
    }
    
    public class ResultsRepository : IResultsRepository
    {
        private readonly HttpClient _httpClient = new HttpClient();
        const string URL = "https://www.sportdata.org/wkf/set-online/popup_main.php?popup_action=results&vernr=276&active_menu=calendar";

        public async Task<HtmlDocument> GetHtmlResultsAsync()
        {
            var html = await _httpClient.GetStringAsync(URL);

            var document = new HtmlDocument();
            document.LoadHtml(html);

            return document;
        }
    }
}