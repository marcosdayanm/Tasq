using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasq.Models
{
	public class Sede
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Generado automático de PK
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? FotoUrl { get; set; }

        // Fk a Direccion
        [ForeignKey("Direccion")]
        public int? IdDireccion { get; set; }
        public Direccion? Direccion { get; set; }

        // One to many
        public ICollection<Departamento>? Departamentos { get; set; }
        public ICollection<AppUser>? Users { get; set; }

    }
}

