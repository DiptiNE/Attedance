using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParentTeacherBridge.API.Data;
using ParentTeacherBridge.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TeacherRepository : ITeacherRepository
{
    private readonly ParentTeacherBridgeAPIContext _context;

    private readonly ILogger<TeacherRepository> _logger;

    public TeacherRepository(ParentTeacherBridgeAPIContext context, ILogger<TeacherRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    //public async Task<IEnumerable<Teacher>> GetAllAsync()
    //{
    //    _logger.LogInformation("Fetching all teachers from the database.");
    //    return await _context.Teacher.ToListAsync();
    //}

    //public async Task<bool> CreateAsync(Teacher teacher)
    //{
    //    _logger.LogInformation("Creating new teacher with name {TeacherName}", teacher.Name);
    //    _context.Teacher.Add(teacher);
    //    var result = await _context.SaveChangesAsync();
    //    _logger.LogInformation("Create operation result: {Result}", result > 0);
    //    return result > 0;
    //}

    public async Task<Teacher> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching teacher with ID {TeacherId}", id);
        return await _context.Teacher.FindAsync(id);
    }

    

    public async Task<bool> UpdateAsync(Teacher teacher)
    {
        _logger.LogInformation("Updating teacher with ID {TeacherId}", teacher.TeacherId);
        _context.Teacher.Update(teacher);
        var result = await _context.SaveChangesAsync();
        _logger.LogInformation("Update operation result: {Result}", result > 0);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Deleting teacher with ID {TeacherId}", id);
        var teacher = await _context.Teacher.FindAsync(id);
        if (teacher == null)
        {
            _logger.LogWarning("Teacher with ID {TeacherId} not found.", id);
            return false;
        }

        _context.Teacher.Remove(teacher);
        var result = await _context.SaveChangesAsync();
        _logger.LogInformation("Delete operation result: {Result}", result > 0);
        return result > 0;
    }
}