using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CChica.Models
{
    public class AddCajaChica
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Codigo")]
        public string IdentityCajaChica { get; set; }

        [Required]
        [StringLength(20), MinLength(3)]
        [DisplayName("Identificación CCH")]
        public string nameCajaChica { get; set; }


        [StringLength(50), MinLength(3)]
        [DisplayName("Creado por")]
        public string CreadoPor { get; set; }

        public bool Validada { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha")]
        public DateTime fecha { get; set; }

        [Range((double)decimal.MinValue,
      (double)decimal.MaxValue,
      ErrorMessage = "Solo se permiten valores decimales")]
        [DataType(DataType.Currency)]
        [DisplayName("Apertura")]
        public decimal AperturaCaja { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de apertura")]
        public DateTime? fechaApertura { get; set; }


        [Range((double)decimal.MinValue,
       (double)decimal.MaxValue,
       ErrorMessage = "Solo se permiten valores decimales")]
        [DataType(DataType.Currency)]
        //[RegularExpression(@"[0-9]*\.?[0-9]*", ErrorMessage = "Solo se permiten valores numericos")]
        [DisplayName("Cierre")]
        public decimal CierreCaja { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de cierre")]
        public DateTime? FechaCierre { get; set; }


        public virtual IEnumerable<EgresoIngreso> EgresoIngresos { get; set; }



    }
}