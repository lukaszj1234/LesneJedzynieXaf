using LesneJedzynie1XAF.Blazor.Server.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesneJedzynie.Models
{
    public class WysylkaDTO
    {
        public Guid IdDziecka { get; set; }
        public List<DzienDTO> Dni { get; set; }
    }
}
