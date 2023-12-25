using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; // Se agregó esta picandole a DbContext
using Tasq.Models;

// Ésta clase es muy importante, nos va a permitir meter y sacar cosas de la Db cuando queramos
// Debemos de añadir esta clase a la app para que la podamos usar, en el archivo Program.cs





namespace Tasq.Data
{
	public class ApplicationDbContext
	{
		public ApplicationDbContext()
		{
		}
	}
}

