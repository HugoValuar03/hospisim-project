using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public enum StatusInternacao
    {
        [Display(Name = "Ativa")]
        ATIVA,

        [Display(Name = "Alta Concedida")]
        ALTA_CONCEDIDA,

        [Display(Name = "Transferido")]
        TRANSFERIDO,

        [Display(Name = "Óbito")]
        OBITO
    }
}
