using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public enum Status
    {
        [Display(Name = "Realizado")]
        REALIZADO = 0,

        [Display(Name = "Em Andamento")]
        EM_ANDAMENTO = 1,

        [Display(Name = "Cancelado")]
        CANCELADO = 2,
    }
}