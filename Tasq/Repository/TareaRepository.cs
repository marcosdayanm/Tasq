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

        // Añadir a la Tabla
        public bool Add(Tarea tarea)
        {
            _context.Add(tarea); 
            return Save();
        }

        // Eliminar elemento de la tabla
        public bool Delete(Tarea tarea)
        {
            _context.Remove(tarea);
            return Save();
        }

        // Seleccionar todos los elementos de la tabla
        public async Task<IEnumerable<Tarea>> GetAll()
        {
            return await _context.Tareas.Include(i => i.User).Include(j => j.Departamento).ThenInclude(d => d.Sede).OrderBy(t => t.FechaEntrega).ToListAsync();
        }

        // Seleccionar todos los elementos de la tabla sin guardar el rastro de la consulta, más eficiente si no necesitamos editar 
        public Task<IEnumerable<Tarea>> GetAllAsyncNoTracking()
        {
            throw new NotImplementedException();
        }

        // Seleccionar todas las tareas sin include, ordenadas por fecha
        public async Task<IEnumerable<Tarea>> GetAllOrderedByDate()
        {
            return await _context.Tareas.OrderBy(t => t.FechaEntrega).ToListAsync();
        }

        // Seleccionar un elemento por su Id
        public async Task<Tarea> GetByIdAsync(int id)
        {
            return await _context.Tareas.Include(j => j.Departamento).ThenInclude(d => d.Sede).FirstOrDefaultAsync(t => t.Id == id);
        }

        // Seleccionar un elemento por su Id sin guardar el rastro de la consulta, más eficiente si no necesitamos editar 
        public Task<Tarea> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }

        // Seleccionar elementos por Id del Departamento
        public async Task<IEnumerable<Tarea>> GetTareasByIdDepartamento(int id)
        {
            return await _context.Tareas.Include(i => i.User).Include(j => j.Departamento).OrderBy(t => t.FechaEntrega).Where(u => u.IdDepartamento == id).ToListAsync();
        }

        // Seleccionar elementos por Id de la Sede
        public async Task<IEnumerable<Tarea>> GetTareasByIdSede(int id)
        {
            return await _context.Tareas.Include(i => i.User).Include(j => j.Departamento).OrderBy(t => t.FechaEntrega).Include(i => i.User).Where(u => u.Departamento.IdSede == id).ToListAsync();
        }

        // Seleccionar elementos por Id del Usuario
        public async Task<IEnumerable<Tarea>> GetTareasByIdUser(string id)
        {
            return await _context.Tareas.Include(i => i.User).Include(j => j.Departamento).OrderBy(t => t.FechaEntrega).Include(i => i.User).Where(u => u.User.Id == id).ToListAsync();
        }

        // Guardar cambios en la Tabla
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        // Actualizar cambios en el elemento en la tabla
        public bool Update(Tarea tarea)
        {
            _context.Update(tarea);
            return Save();
        }
    }
}

