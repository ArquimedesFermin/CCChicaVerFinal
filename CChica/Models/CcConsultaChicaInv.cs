using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CChica.Models
{
    [NotMapped]
    public class CcConsultaChicaInv
    {
        public string id { get; set; }
        public int idCajaChica { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime Fecha { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Fecha")]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public string Time { get; set; }
        public string Ingreso  { get; set; }
        public string Gasto { get; set; }
        public String Descripcion { get; set; }
        public string Concepto { get; set; }
        public string cajaChica { get; set; }
    }
}