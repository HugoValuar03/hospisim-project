using System.Collections.Generic;

namespace Hospisim.Models
{
    public class DashboardViewModel
    {
        public int PacientesInternados { get; set; }
        public int AtendimentosHoje { get; set; }
        public int LeitosOcupados { get; set; }
        public int ProfissionaisAtivos { get; set; }

        public List<Internacao> UltimasInternacoes { get; set; }
        public List<AltaHospitalar> UltimasAltas { get; set; }

        public DashboardViewModel()
        {
            UltimasInternacoes = new List<Internacao>();
            UltimasAltas = new List<AltaHospitalar>();
        }
    }
}