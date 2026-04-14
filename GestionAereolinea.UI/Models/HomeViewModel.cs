// ViewModel de la página de inicio
// Un ViewModel es una clase que se usa únicamente para transportar
// datos desde el Controlador hacia la Vista, sin tocar la base de datos
namespace GestionAereolinea.UI.Models
{
    public class HomeViewModel
    {
        // Cantidad total de aerolíneas registradas en el sistema
        public int TotalAerolineas { get; set; }

        // Cantidad total de aviones registrados sin importar su estado
        public int TotalAviones { get; set; }

        // Cantidad de aviones cuyo estado es "Activo"
        public int TotalAvionesActivos { get; set; }

        // Cantidad de aviones cuyo estado es "InActivo"
        public int TotalAvionesInactivos { get; set; }
    }
}