using System;
using Microsoft.EntityFrameworkCore;
using Tasq.Data;
using Tasq.Interfaces;
using Tasq.Models;

namespace Tasq.Repository
{
	public class TareaRepository : ITareaRepository
	{

        private readonly ApplicationDbContext _context;

        public TareaRepository(ApplicationDbContext c)
		{
            _context = c;
        }

        public bool Add(Tarea tarea)
        {
            _context.Add(tarea); 
            return Save();
        }

        public bool Delete(Tarea tarea)
        {
            _context.Remove(tarea);
            return Save();
        }

        public async Task<IEnumerable<Tarea>> GetAll()
        {
            return await _context.Tareas.ToListAsync();
        }

        public Task<IEnumerable<Tarea>> GetAllAsyncNoTracking()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tarea>> GetAllOrderedByDate()
        {
            return await _context.Tareas.OrderByDescending(t => t.FechaEntrega).ToListAsync();
        }

        public async Task<Tarea> GetByIdAsync(int id)
        {
            return await _context.Tareas.FindAsync(id);
        }

        public Task<Tarea> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tarea>> GetTareasByIdDepartamento(int id)
        {
            return await _context.Tareas.Where(u => u.IdDepartamento == id).ToListAsync();
        }

        public async Task<IEnumerable<Tarea>> GetTareasByIdSede(int id)
        {
            return await _context.Tareas.Where(u => u.Departamento.IdSede == id).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Tarea tarea)
        {
            _context.Update(tarea);
            return Save();
        }
    }
}

