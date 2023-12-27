using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Tasq.ViewModels
{
	public class LoginVM
	{
        [Display(Name = "Email")]
        [Required(ErrorMessage = "La dirección de Email es requerida")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La Contraseña es requerida")]
        [DataType(DataType.Password)] // Ésto es validation
        public string Password { get; set; }
    }
}

