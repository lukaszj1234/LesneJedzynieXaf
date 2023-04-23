namespace LesneJedzynie1XAF.Blazor.Server.DTO.Podlewaczka
{
    public class GetOdczytPodlewaczkaDTO
    {
        public DateTime DataOdczytu { get; set; }
        public DateTime RozpoczeciePodlewania { get; set; }
        public DateTime ZakonczeniePodlewania { get; set; }
        public double Napiecie { get; set; }
        public double PoziomWody { get; set; }
        public double PoziomWodyRozpoczeciePodlewania { get; set; }
        public int Wilgotnosc { get; set; }
    }
}
