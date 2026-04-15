
using System.ComponentModel.DataAnnotations;


namespace GestionAereolinea.Model
{
    public class Aerolinea // Clase que representa una aerolínea (entidad de la base de datos)
    {
        [Key] // Indica que esta propiedad es la clave primaria (ID único)
        public int Id { get; set; } // Identificador único de la aerolínea

        [Required(ErrorMessage = "El nombre de la aerolínea es requerido")] // Campo obligatorio
        public string Nombre { get; set; } // Nombre de la aerolínea

        [Required(ErrorMessage = "El Telefono es requerido")] // Campo obligatorio
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "El telefono debe tener 8 números")] // Validación: solo 8 números
        public string Telefono { get; set; } // Teléfono de la aerolínea

        [Required(ErrorMessage = "El código de la aerolínea es requerido")] // Campo obligatorio
        public string Codigo { get; set; } // Código identificador de la aerolínea

        [Required(ErrorMessage = "El país de la aerolínea es requerido")] // Campo obligatorio
        public string PaísOrigen { get; set; } // País de origen de la aerolínea

        public List<Avion>? Aviones { get; set; } // Relación: lista de aviones asociados a la aerolínea (puede ser null)
    }
}
