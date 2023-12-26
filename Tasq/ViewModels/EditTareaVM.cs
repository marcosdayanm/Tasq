using System;
using Tasq.Models;

namespace Tasq.ViewModels
{
	public class EditTareaVM
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? FotoUrl { get; set; }
        public int IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }
        public Departamento? Departamento { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
}

