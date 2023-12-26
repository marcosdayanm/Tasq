using System;
using Tasq.Models;

namespace Tasq.ViewModels
{
	public class DetailSedeVM
	{
        public Sede Sede { get; set; }
        public IEnumerable<Departamento> Departamentos { get; set; }
    }
}

