using System;
using System.ComponentModel.DataAnnotations;
using Tasq.Models;

namespace Tasq.ViewModels
{
	public class EditTareaVM
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre es Requerido")]
        public string Nombre { get; set; }

        public string? FotoUrl { get; set; }
        public int IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }
        public Departamento? Departamento { get; set; }
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Fecha de entrega es Requerida")]
        public DateTime FechaEntrega { get; set; }
    }
}

