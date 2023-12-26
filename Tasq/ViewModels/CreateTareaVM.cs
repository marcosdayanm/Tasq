using System;
namespace Tasq.ViewModels
{
	public class CreateTareaVM
	{
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? FotoUrl { get; set; }
        public string? Descripcion { get; set; }
        public int IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }
        public DateTime? FechaEntrega { get; set; }
    }
}

