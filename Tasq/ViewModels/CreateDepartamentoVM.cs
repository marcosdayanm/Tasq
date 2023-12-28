using System;
using Tasq.Models;

namespace Tasq.ViewModels
{
	public class CreateDepartamentoVM
	{
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? FotoUrl { get; set; }
        public string? Descripcion { get; set; }
        public int IdSede { get; set; }
        public string? NombreSede { get; set; }
    }
}

