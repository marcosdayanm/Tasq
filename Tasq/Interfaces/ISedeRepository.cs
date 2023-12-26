using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasq.Models; // Añadido

// Checar el archivo de notes.md


namespace Tasq.Interfaces
{
    public interface ISedeRepository
    {
        // Consultas a la database
        Task<IEnumerable<Sede>> GetAll();
        Task<Sede> GetByIdAsync(int id);
        Task<IEnumerable<Sede>> GetSedeByCity(string city);
        // Task<Sede> GetByIdAsyncNoTracking(int id);

        // CRUD
        // Por convención las funciones CRUD siempre van hasta abajo
        bool Add(Sede sede);
        bool Update(Sede sede);
        bool Delete(Sede sede);
        bool Save();
    }
}

