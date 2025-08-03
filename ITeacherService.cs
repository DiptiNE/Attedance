using ParentTeacherBridge.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITeacherService
{
    //Task<IEnumerable<Teacher>> GetAllTeachersAsync();
    //Task<bool> CreateTeacherAsync(Teacher teacher);
    Task<Teacher> GetTeacherByIdAsync(int id);
    Task<bool> UpdateTeacherAsync(Teacher teacher);
    Task<bool> DeleteTeacherAsync(int id);
}