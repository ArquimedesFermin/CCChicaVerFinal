using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CChica.Models
{
    public class TipoEstado
    {
        [Key]
        public int Id { get; set; }

        [StringLength (20),MinLength(3)]
        [RegularExpression(@"^[A-Z]+[a-z]*$", ErrorMessage ="Este campo no acepta caracteres numericos")]
        public string Estado { get; set; }

        public virtual IEnumerable<EgresoIngreso> EgresoIngresos { get; set; }
    }
}