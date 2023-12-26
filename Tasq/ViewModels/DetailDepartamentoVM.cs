using System;
using Tasq.Models;

namespace Tasq.ViewModels
{
	public class DetailDepartamentoVM
	{
        public Departamento Departamento { get; set; }
        public IEnumerable<Tarea> Tareas { get; set; }
    }
}

