using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace Tasq.Models
{
	public class Departamento
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? FotoUrl { get; set; }

        // FK a Sede
        [ForeignKey("Sede")]
        public int IdSede { get; set; }
        public Sede Sede { get; set; }

        // One to many
        public ICollection<Tarea>? Tareas { get; set; }
        public ICollection<AppUser>? Users { get; set; }
    }
}

