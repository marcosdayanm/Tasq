using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; // Se agregó esta picandole a DbContext
using Tasq.Models;

// Ésta clase es muy importante, nos va a permitir meter y sacar cosas de la Db cuando queramos
// Debemos de añadir esta clase a la app para que la podamos usar, en el archivo Program.cs


namespace Tasq.Data
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
		}

        public DbSet<Sede> Sedes { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Models.Task> Tasks { get; set; } // para evitar errores



        // Aclarar las relaciones One to Many
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Sede a Departamento
            modelBuilder.Entity<Sede>()
                .HasMany(s => s.Departamentos)
                .WithOne(d => d.Sede)
                .HasForeignKey(d => d.IdSede);


            // Sede a AppUser
            modelBuilder.Entity<Sede>()
                .HasMany(s => s.Users)
                .WithOne(u => u.Sede)
                .HasForeignKey(u => u.IdSede);

            // Departamento a Task
            modelBuilder.Entity<Departamento>()
                .HasMany(d => d.Tasks)
                .WithOne(t => t.Departamento)
                .HasForeignKey(t => t.IdDepartamento);

            // AppUser a Task
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.IdUser);
        }



    }
}


