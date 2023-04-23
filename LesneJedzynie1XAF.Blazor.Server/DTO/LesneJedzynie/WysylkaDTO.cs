using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesneJedzynie1XAF.Blazor.Server.DTO.LesneJedzynie
{
    public class WysylkaDTO
    {
        public Guid IdDziecka { get; set; }
        public List<DzienDTO> Dni { get; set; }
    }
}
