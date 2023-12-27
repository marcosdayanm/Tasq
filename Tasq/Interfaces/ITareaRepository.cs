using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasq.Models; // Añadido

// Checar el archivo de notes.md


namespace Tasq.Interfaces
{
    public interface ITareaRepository
    {
        // Consultas a la database
        Task<IEnumerable<Tarea>> GetAll();
        Task<IEnumerable<Tarea>> GetAllAsyncNoTracking();
        Task<IEnumerable<Tarea>> GetAllOrderedByDate();
        Task<Tarea> GetByIdAsync(int id);
        Task<Tarea> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Tarea>> GetTareasByIdSede(int id);
        Task<IEnumerable<Tarea>> GetTareasByIdDepartamento (int id);
        Task<IEnumerable<Tarea>> GetTareasByIdUser(string id);

        // CRUD
        bool Add(Tarea tarea);
        bool Update(Tarea tarea);
        bool Delete(Tarea tarea);
        bool Save();
    }
}

