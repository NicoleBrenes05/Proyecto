using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace GestionAereolinea.Model
{
    public class Aerolinea
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la aerolínea es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Telefono es requerido")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "El telefono debe tener 8 números")]
       
        public string Telefono { get; set; }

        public List<Avion>? Aviones { get; set; }

    }
}
