using ParentTeacherBridge.API.Models;

public interface ITimetableService
{
    Task<IEnumerable<Timetable>> GetAllTimetablesAsync();
    Task<Timetable?> GetTimetableByIdAsync(int id);
}