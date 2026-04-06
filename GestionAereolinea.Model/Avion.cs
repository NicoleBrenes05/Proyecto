
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace GestionAereolinea.Model
{
    [Table("Avion")]
    public class Avion
    {
        [Key]
        public int Id { get; set; }

       [Required(ErrorMessage = "El nombre del avión es requerido")]
       public string Nombre { get; set; }


      [Required(ErrorMessage = "El modelo es requerido")]
      public string Modelo { get; set; }

        public int Capacidad { get; set; }

        [ForeignKey("Aerolinea")]
        public int AerolineaId { get; set; }

        public Aerolinea? Aerolinea { get; set; }
       
        public Estado Estado { get; set; }
    }
}
