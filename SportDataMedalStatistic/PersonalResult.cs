namespace SportDataMedalStatistic
{
    public class PersonalResult
    {
        public string Category { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Club { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"{Country} {Category} {Name} - {Rank}";
        }
    }
}