
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace GestionAereolinea.Model
{
    [Table("Avion")]
    public class Avion // Clase que representa la entidad Avion
    {
        [Key] // Indica que esta propiedad es la clave primaria
        public int Id { get; set; } // Identificador único del avión

        [Required(ErrorMessage = "El nombre del avión es requerido")] // Campo obligatorio
        public string Nombre { get; set; } // Nombre del avión

        [Required(ErrorMessage = "El modelo es requerido")] // Campo obligatorio
        public string Modelo { get; set; } // Modelo del avión

        public int Capacidad { get; set; } // Capacidad de pasajeros del avión

        [ForeignKey("Aerolinea")] // Indica que esta propiedad es clave foránea hacia Aerolinea
        public int AerolineaId { get; set; } // ID de la aerolínea a la que pertenece el avión

        public Aerolinea? Aerolinea { get; set; } // Propiedad de navegación (relación con Aerolinea)

        public Estado Estado { get; set; } // Estado del avión (por ejemplo: Activo o Inactivo)
    }
}
