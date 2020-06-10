using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CChica.Models
{
    public class EgresoIngreso
    {
        [Key]
        [DisplayName("Codigo")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Caja chica")]
        public int IdCajaChica { get; set; }

        [Range((double)decimal.MinValue,
               (double)decimal.MaxValue,
               ErrorMessage = "Solo se permiten valores decimales")]
        [DataType(DataType.Currency)]     
        [Required]
        [DisplayName("Monto")]
        public decimal Dinero { get; set; }

        [DisplayName("Estado")]
        public int IdEstado { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime? Fecha { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Hora")]
        public DateTime Time { get; set; }

        [StringLength(Int32.MaxValue,MinimumLength =3,ErrorMessage ="Este campo debe de tener maximo 200 caracteres y minimo 3 caracteres")]
        [Required]
        public string Concepto { get; set; }

        [StringLength(int.MaxValue),MinLength(3,ErrorMessage ="Debe tener un minimo de 3 caracteres")]
        [DisplayName("Descripción")]
        [Required]
        public string Descripcion { get; set; }

        [ForeignKey("IdEstado")]
        public virtual TipoEstado TipoEstado { get; set; }

        [ForeignKey("IdCajaChica")]
        public virtual AddCajaChica AddCajaChica { get; set; }
    }
}