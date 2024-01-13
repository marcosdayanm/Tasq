using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Tasq.Data;
using Tasq.Interfaces;
using Tasq.Models;

namespace Tasq.Repository
{
	public class DepartamentoRepository : IDepartamentoRepository
    {

        private readonly ApplicationDbContext _context;

        public DepartamentoRepository(ApplicationDbContext c)
        {
            _context = c;
        }

        // Añadir a la Tabla
        public bool Add(Departamento departamento)
        {
            _context.Add(departamento);
            return Save();
        }

        // Eliminar de la Tabla
        public bool Delete(Departamento departamento)
        {
            _context.Remove(departamento);
            return Save();
        }

        // Seleccionar todos los elementos de la tabla
        public async Task<IEnumerable<Departamento>> GetAll()
        {
            return await _context.Departamentos.Include(i => i.Sede).ToListAsync();
        }

        // Seleccionar todos los elementos de la tabla sin guardar el rastro de la consulta, más eficiente si no necesitamos editar 
        public Task<IEnumerable<Departamento>> GetAllAsyncNoTracking()
        {
            throw new NotImplementedException();
        }

        // Seleccionar un elemento por su Id
        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _context.Departamentos.Include(i => i.Sede).FirstOrDefaultAsync(i => i.Id == id);
        }

        // Seleccionar un elemento por su Id sin guardar el rastro de la consulta
        public async Task<Departamento> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Departamentos.AsNoTracking().Include(i => i.Sede).FirstOrDefaultAsync(i => i.Id == id);
        }

        // Seleccionar el departamento por el Id de la Sede
        public async Task<IEnumerable<Departamento>> GetDepartamentosByIdSede(int id)
        {
            return await _context.Departamentos.Where(u => u.IdSede == id).ToListAsync();
        }

        // Guardar cambios en la Tabla
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        // Actualizar cambios en el elemento en la tabla
        public bool Update(Departamento departamento)
        {
            _context.Update(departamento);
            return Save();
        }
    }
}

