using System;
using Tasq.Models;

namespace Tasq.Interfaces
{
	public interface IDepartamentoRepository
	{
        // Consultas a la database
        Task<IEnumerable<Departamento>> GetAll();
        Task<IEnumerable<Departamento>> GetAllAsyncNoTracking();
        Task<Departamento> GetByIdAsync(int id);
        Task<Departamento> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Departamento>> GetDepartamentosByIdSede(int id);

        // CRUD
        bool Add(Departamento departamento);
        bool Update(Departamento departamento);
        bool Delete(Departamento departamento);
        bool Save();
    }
}

