using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using Microsoft.AspNetCore.Identity;

namespace Tasq.Models
{
	public class AppUser : IdentityUser // Para el IdentityFramework
	{
        //[Key]
        //public string Id { get; set; } 

        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? FormacionProfesional { get; set; }
        public string? FotoUrl { get; set; }

        // FK a Sede
        [ForeignKey("Sede")]
        public int? IdSede { get; set; }
        public Sede? Sede { get; set; }

        // One to Many
        public ICollection<Task>? Tasks { get; set; }

    }
}

