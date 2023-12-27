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

        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.Include(i => i.Departamento).Include(i => i.Sede).ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersByCity(string city)
        {
            return await _context.Users.Where(c => c.Direccion.Ciudad.Contains(city)).ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersByIdDepartamento(int id)
        {
            return await _context.Users.Where(u => u.IdDepartamento == id).ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersByIdSede(int id)
        {
            return await _context.Users.Where(s => s.IdSede == id).ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.Include(u => u.Sede).FirstOrDefaultAsync(u => u.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}

