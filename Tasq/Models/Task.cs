using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasq.Models
{
	public class Task
	{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? FotoUrl { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEntrega { get; set; }

        // Fk a AppUser
        [ForeignKey("AppUser")]
        public string? IdUser { get; set; }
        public AppUser? User { get; set; }

        // Fk a Departamento
        [ForeignKey("Departamento")]
        public int? IdDepartamento { get; set; }
        public Departamento? Departamento { get; set; }
    }
}

