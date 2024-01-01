using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tasq.Models;

namespace Tasq.ViewModels
{
    public class EditUserVM
    {

        public string Id { get; set; }

        [Display(Name = "* Nombre Completo")]
        [Required(ErrorMessage = "Nombre es requerido, intente de nuevo")]
        public string Nombre { get; set; }

        [Display(Name = "* Email")]
        [Required(ErrorMessage = "Email es requerido, intente de nuevo")]
        public string Email { get; set; }

        [Display(Name = "Fecha de Nacimiento (Opcional)")]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "Formación Profesional (Opcional)")]
        public string? FormacionProfesional { get; set; }

        [Display(Name = "Foto de Perfil (URL, Opcional)")]
        public string? FotoUrl { get; set; }

        [Display(Name = "* Selecciona una Sede")]
        public int SedeSeleccionadaId { get; set; }

        [Display(Name = "* Selecciona una Sede")]
        public IEnumerable<Sede>? Sedes { get; set; }

    }
}

