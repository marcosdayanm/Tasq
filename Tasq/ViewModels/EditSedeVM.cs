using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasq.Models;

namespace Tasq.ViewModels
{
	public class EditSedeVM
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre es Requerido")]
        public string Nombre { get; set; }
        public string? FotoUrl { get; set; }
        public int? IdDireccion { get; set; }
        public Direccion? Direccion { get; set; }
        public string? Descripcion { get; set; }

    }
}

