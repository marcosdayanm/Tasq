using System;
using System.ComponentModel.DataAnnotations;

namespace Tasq.ViewModels
{
	public class CreateTareaVM
	{
        public int? Id { get; set; }

        [Required(ErrorMessage = "Nombre es Requerido")]
        public string Nombre { get; set; }
        public string? FotoUrl { get; set; }
        public string? Descripcion { get; set; }
        public int IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }

        [Required(ErrorMessage = "Fecha de entrega es Requerida")]
        public DateTime FechaEntrega { get; set; }
    }
}

