
using System.ComponentModel.DataAnnotations;


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
  
        [Required(ErrorMessage = "El código de la aerolínea es requerido")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El país de la aerolínea es requerido")]
        public string PaísOrigen {  get; set; }

        public List<Avion>? Aviones { get; set; }

        

    }
}
