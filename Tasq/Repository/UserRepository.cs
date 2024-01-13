using System;
using Microsoft.EntityFrameworkCore;
using Tasq.Data;
using Tasq.Interfaces;
using Tasq.Models;

namespace Tasq.Repository
{
	public class UserRepository : IUserRepository
	{
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext c)
		{
            _context = c;
        }

        // Añadir a la Tabla
        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        // Eliminar elemento de la tabla
        public bool Delete(AppUser user)
        {
            _context.Remove(user);
            return Save();
        }

        // Seleccionar todos los elementos de la tabla
        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.Include(i => i.Departamento).Include(i => i.Sede).ToListAsync();
        }

        // Seleccionar todos los elementos de la tabla por su Ciudad
        public async Task<IEnumerable<AppUser>> GetAllUsersByCity(string city)
        {
            return await _context.Users.Where(c => c.Direccion.Ciudad.Contains(city)).ToListAsync();
        }

        // Seleccionar todos los elementos de la tabla por su departamento
        public async Task<IEnumerable<AppUser>> GetAllUsersByIdDepartamento(int id)
        {
            return await _context.Users.Where(u => u.IdDepartamento == id).ToListAsync();
        }

        // Seleccionar todos los elementos de la tabla por su sede
        public async Task<IEnumerable<AppUser>> GetAllUsersByIdSede(int id)
        {
            return await _context.Users.Where(s => s.IdSede == id).ToListAsync();
        }

        // Seleccionar elemento de la tabla por su Id 
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.Include(u => u.Sede).FirstOrDefaultAsync(u => u.Id == id);
        }

        // Seleccionar elemento de la tabla por su Id sin guardar el rastro de la consulta, más eficiente si no necesitamos editar 
        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users.AsNoTracking().Include(u => u.Sede).FirstOrDefaultAsync(u => u.Id == id);
        }

        // Guardar cambios en la tabla
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        // Actualizar cambios en el elemento en la tabla
        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}

