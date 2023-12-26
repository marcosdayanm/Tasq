using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasq.Models
{
	public class Tarea
	{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? FotoUrl { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEntrega { get; set; }

        [Range(1, 5, ErrorMessage = "La prioridad debe estar entre 1 y 5")]
        public int Prioridad { get; set; }

        // Fk a AppUser
        [ForeignKey("AppUser")]
        public string? IdUser { get; set; }
        public AppUser? User { get; set; }

        // Fk a Departamento
        [ForeignKey("Departamento")]
        public int IdDepartamento { get; set; }
        public Departamento? Departamento { get; set; }
    }
}

