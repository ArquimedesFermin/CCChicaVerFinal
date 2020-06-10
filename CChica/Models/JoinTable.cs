using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CChica.Models
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public class JoinTable
    {
        public AddCajaChica AddCajaChicav { get; set; }
        public CcConsultaChicaInv CcConsultaChicaInvv { get; set; }
    }
}