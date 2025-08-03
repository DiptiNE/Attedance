using ParentTeacherBridge.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITeacherRepository
{
    //Task<IEnumerable<Teacher>> GetAllAsync();
    //Task<bool> CreateAsync(Teacher teacher);
    Task<Teacher> GetByIdAsync(int id);
    Task<bool> UpdateAsync(Teacher teacher);
    Task<bool> DeleteAsync(int id);
}