using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tasq.Models;

namespace Tasq.ViewModels
{
	public class CreateSedeVM
	{
        public string Nombre { get; set; }
        public string? FotoUrl { get; set; }
        public Direccion? Direccion { get; set; }
        public string? Descripcion { get; set; }

    }
}

