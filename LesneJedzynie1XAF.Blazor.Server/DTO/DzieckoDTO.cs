using LesneJedzynie1XAF.Enums;

namespace LesneJedzynie1XAF.Blazor.Server.DTO
{
    public class DzieckoDTO
    {
        public Guid IdDziecka { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public Plec Plec { get; set; }
    public List<DzienDTO> Dni { get; set; }
    }
}
