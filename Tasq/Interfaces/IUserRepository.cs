using System;
using Tasq.Models;

namespace Tasq.Interfaces
{
	public interface IUserRepository
	{
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string id);
        Task<IEnumerable<AppUser>> GetAllUsersByCity(string city);
        Task<IEnumerable<AppUser>> GetAllUsersByIdSede(int id);
        Task<IEnumerable<AppUser>> GetAllUsersByIdDepartamento(int id);

        // CRUD
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}

