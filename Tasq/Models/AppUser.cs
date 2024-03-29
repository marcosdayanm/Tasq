﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using Microsoft.AspNetCore.Identity;

namespace Tasq.Models
{
	public class AppUser : IdentityUser // Para el IdentityFramework
	{
        // No es necesario que el IdentityFramework genera el ID
        //[Key]
        //public string Id { get; set; } 

        public string Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? FormacionProfesional { get; set; }
        public string? FotoUrl { get; set; }

        // FK a Sede
        [ForeignKey("Sede")]
        public int IdSede { get; set; }
        public Sede? Sede { get; set; }

        // FK a Departamento
        [ForeignKey("Departamento")]
        public int? IdDepartamento { get; set; }
        public Departamento? Departamento { get; set; }

        // Fk a Direccion
        [ForeignKey("Direccion")]
        public int? IdDireccion { get; set; }
        public Direccion? Direccion { get; set; }

        // One to Many
        public ICollection<Tarea>? Tareas { get; set; }

    }
}

