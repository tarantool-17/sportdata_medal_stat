using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace SportDataMedalStatistic
{
    public interface IPersonalResultParser
    {
        List<PersonalResult> Parse(HtmlDocument document);
    }
    public class PersonalResultParser : IPersonalResultParser
    {
        public List<PersonalResult> Parse(HtmlDocument document)
        {
            if(document == null)
                throw new ArgumentNullException();
            
            var mainTable = document.DocumentNode.SelectSingleNode("//table[contains(@class,'moduletable')]");
            var rows = mainTable.SelectNodes("//tr[contains(@class, 'dctabrowwhite') or contains(@class, 'dctabrowgreen')]");

            var results = new List<PersonalResult>();
            foreach(var row in rows)
            {
                var tds = row.ChildNodes.Where(x => x.Name == "td").ToList();

                if(tds.Count < 4)
                    continue;

                results.Add(new PersonalResult 
                {
                    Category = tds[0].InnerText?.Replace("&nbsp;", String.Empty),
                    Rank = Int32.TryParse(tds[1].InnerText?.Replace("&nbsp;", String.Empty), out var rank) ? rank : -1,
                    Name = tds[2].InnerText?.Replace("&nbsp;", String.Empty),
                    Club = tds[3].InnerText?.Replace("&nbsp;", String.Empty),
                    Country = tds[4].InnerText?.Replace("&nbsp;", String.Empty)
                });
            }

            return results;
        }
    }
}