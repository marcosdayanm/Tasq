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
        public string? FotoUrl { get; set; }

        // FK a Sede
        [ForeignKey("Sede")]
        public int? IdSede { get; set; }
        public Sede? Sede { get; set; }

        // One Departamento to Many Tasks
        public ICollection<Task>? Tasks { get; set; }
    }
}

