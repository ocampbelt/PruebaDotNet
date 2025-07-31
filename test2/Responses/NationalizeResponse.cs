namespace test2.Responses
{

    public class NationalizeCountry
    {
        public string Country_Id { get; set; } = string.Empty;
        public double Probability { get; set; }
    }

    public class NationalizeResponse
    {
        public string Name { get; set; } = string.Empty;
        public List<NationalizeCountry> Country { get; set; } = new List<NationalizeCountry>();

    }
}
