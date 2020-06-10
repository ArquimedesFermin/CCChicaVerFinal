using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CChica.Models
{
    [NotMapped]
    public class ConsultaCChica
    {
        public int id { get; set; }
        public int idCajaChicau { get; set; }
        public string cajachica { get; set; }
        public string Concepto { get; set; }
        public string estado { get; set; }
        public DateTime fecha { get; set; }
        public DateTime Time { get; set; }
        public decimal Dinero { get; set; }
        public string Descripcion { get; set; }




    }
}