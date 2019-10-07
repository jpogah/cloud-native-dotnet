namespace FortuneTeller.UI.Services
{
    public class FortuneServiceOptions
    {
        public string Scheme { get; set; } = "http";

        public string BaseAddress { get; set; }

        public string RandomFortunePath { get; set; }

        public string AllFortunesPath { get; set; }

        public string RandomFortuneURL
        {
            get
            {
                return MakeUrl(RandomFortunePath);
            }
        }

        public string AllFortunesURL
        {
            get
            {
                return MakeUrl(AllFortunesPath);
            }
        }

        private string MakeUrl(string path)
        {
            return Scheme + "://" + BaseAddress + path;
        }
    }
}
