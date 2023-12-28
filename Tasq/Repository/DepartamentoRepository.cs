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


        public bool Add(Departamento departamento)
        {
            _context.Add(departamento);
            return Save();
        }

        public bool Delete(Departamento departamento)
        {
            _context.Remove(departamento);
            return Save();
        }

        public async Task<IEnumerable<Departamento>> GetAll()
        {
            return await _context.Departamentos.Include(i => i.Sede).ToListAsync();
        }

        public Task<IEnumerable<Departamento>> GetAllAsyncNoTracking()
        {
            throw new NotImplementedException();
        }

        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _context.Departamentos.Include(i => i.Sede).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Departamento> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Departamentos.AsNoTracking().Include(i => i.Sede).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Departamento>> GetDepartamentosByIdSede(int id)
        {
            return await _context.Departamentos.Where(u => u.IdSede == id).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;

            //try
            //{
            //    var saved = _context.SaveChanges();
            //    return saved > 0;
            //}
            //catch (Exception ex)
            //{
            //    // Registra la excepción para obtener más detalles
            //    Console.WriteLine(ex.Message);
            //    // Considera también registrar el stack trace si es necesario
            //    return false;
            //}
        }

        public bool Update(Departamento departamento)
        {
            _context.Update(departamento);
            return Save();
        }
    }
}

