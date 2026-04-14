// Modelo que representa la información de un error en el sistema
// ASP.NET lo usa automáticamente cuando ocurre una excepción no controlada
namespace GestionAereolinea.UI.Models
{
    public class ErrorViewModel
    {
        // Identificador único del request que causó el error
        // El "?" indica que puede ser null (si no hay ID disponible)
        public string? RequestId { get; set; }

        // Propiedad calculada: devuelve true si RequestId tiene valor
        // Se usa en la vista de error para decidir si mostrar el ID o no
        // "=>" es una expresión lambda, equivale a: get { return !string.IsNullOrEmpty(RequestId); }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
