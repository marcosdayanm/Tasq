using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasq.Models
{
	public class Direccion
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }

        // FK a sede
        public Sede? Sede { get; set; }
    }
}

