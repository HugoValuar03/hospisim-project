using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospisim.Models
{
    public enum TipoAtendimento
    {
        [Display(Name = "Emergência")]
        EMERGENCIA = 0,

        [Display(Name = "Consulta")]
        CONSULTA = 1,

        [Display(Name = "Internação")]
        INTERNACAO = 2
    }
}