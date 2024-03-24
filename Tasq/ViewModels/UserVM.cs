using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using Tasq.Models;

namespace Tasq.ViewModels
{
	public class UserVM
	{
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? FormacionProfesional { get; set; }
        public string? FotoUrl { get; set; }

        public int? IdSede { get; set; }
        public Sede? Sede { get; set; }

        public IEnumerable<Tarea>? Tareas { get; set; }
    }
}

