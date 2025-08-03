using ParentTeacherBridge.API.Models;

public interface ITimetableRepository
{
    Task<IEnumerable<Timetable>> GetAllAsync();
    Task<Timetable?> GetByIdAsync(int id);
}