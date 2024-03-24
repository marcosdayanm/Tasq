using System;
using Microsoft.EntityFrameworkCore;
using Tasq.Data;
using Tasq.Interfaces;
using Tasq.Models;

namespace Tasq.Repository
{
	public class SedeRepository : ISedeRepository // Para añdir como todos los métodos de la interfaz, le tengo que dar como en el foquito de la heredada de clase, y le pico implementar Interface, o algo así
    {

        private readonly ApplicationDbContext _context;

		public SedeRepository(ApplicationDbContext c)
		{
            _context = c;
        }

        // Añadir a la Tabla
        public bool Add(Sede sede)
        {
            _context.Add(sede); // Cuando llamas a este Add, se genera todo el SQL para guardarlo, y cuando llamas la función save, es cuando ya se guarda en la base de datos
            return Save();
        }

        // Eliminar elemento de la tabla
        public bool Delete(Sede sede)
        {
            _context.Remove(sede);
            return Save();
        }

        // Seleccionar todos los elementos de la tabla
        public async Task<IEnumerable<Sede>> GetAll()
        {
            return await _context.Sedes.ToListAsync();
        }

        // Seleccionar todos los elementos de la tabla sin guardar el rastro de la consulta, más eficiente si no necesitamos editar 
        public async Task<IEnumerable<Sede>> GetAllNoTracking()
        {
            return await _context.Sedes.AsNoTracking().ToListAsync();
        }

        // Seleccionar un elemento por su Id
        public async Task<Sede> GetByIdAsync(int id)
        {
            // Es importante siempre con las FK poner el include, ya que sinó no serán traídos los datos de esa fk, es como si ejecutaramos un JOIN
            return await _context.Sedes.Include(i => i.Direccion).FirstOrDefaultAsync(i => i.Id == id); // Éste solo regresa un elemento, a diferencia a la función de arriba que regresa una lista
        }

        // Seleccionar el elemento por ciudad
        public async Task<IEnumerable<Sede>> GetSedeByCity(string city)
        {
            return await _context.Sedes.Where(c => c.Direccion.Ciudad.Contains(city)).ToListAsync(); // Acá vamos dentro de Sede > Direccion > buscar ciudad
        }

        // Guardar cambios en la Tabla
        public bool Save()
        {
            var saved = _context.SaveChanges(); // La función regresa un booleano, 0 si se pudo guardar
            return saved > 0 ? true : false; // Por eso acá se valida el valor de saved para ver si se pudo guardar o no
        }

        // Actualizar cambios en el elemento en la tabla
        public bool Update(Sede sede)
        {
            _context.Update(sede);
            return Save();
        }
    }
}

