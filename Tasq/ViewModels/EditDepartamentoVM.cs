using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tasq.Models;

namespace Tasq.ViewModels
{
	public class EditDepartamentoVM
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? FotoUrl { get; set; }
        public int IdSede { get; set; }
        public Sede? Sede { get; set; }
        public string? Descripcion { get; set; }

    }
}

