using ParentTeacherBridge.API.Models;
using ParentTeacherBridge.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _repository;

    public TeacherService(ITeacherRepository repository)
    {
        _repository = repository;
    }

    //public async Task<IEnumerable<Teacher>> GetAllTeachersAsync() =>
    //    await _repository.GetAllAsync();

    public async Task<Teacher> GetTeacherByIdAsync(int id) =>
        await _repository.GetByIdAsync(id);

    //public async Task<bool> CreateTeacherAsync(Teacher teacher) =>
    //    await _repository.CreateAsync(teacher);

    public async Task<bool> UpdateTeacherAsync(Teacher teacher) =>
        await _repository.UpdateAsync(teacher);

    public async Task<bool> DeleteTeacherAsync(int id) =>
        await _repository.DeleteAsync(id);
}