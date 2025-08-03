// Services/TimetableService.cs
using ParentTeacherBridge.API.Models;

public class TimetableService : ITimetableService
{
    private readonly ITimetableRepository _repository;

    public TimetableService(ITimetableRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Timetable>> GetAllTimetablesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Timetable?> GetTimetableByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
