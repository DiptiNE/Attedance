// Repositories/TimetableRepository.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParentTeacherBridge.API.Data;
using ParentTeacherBridge.API.Models;

public class TimetableRepository : ITimetableRepository
{
    private readonly ParentTeacherBridgeAPIContext _context;
    private readonly ILogger<TimetableRepository> _logger;

    public TimetableRepository(ParentTeacherBridgeAPIContext context, ILogger<TimetableRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Timetable>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all timetables from database");
        try
        {
            var timetables = await _context.Timetables
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .Include(t => t.Teacher)
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} timetables", timetables.Count);
            return timetables;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all timetables");
            throw;
        }
    }

    public async Task<Timetable?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching timetable with Id {Id}", id);
        try
        {
            var timetable = await _context.Timetables
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(t => t.TimetableId == id);

            if (timetable == null)
                _logger.LogWarning("Timetable with Id {Id} not found", id);
            else
                _logger.LogInformation("Timetable with Id {Id} fetched successfully", id);

            return timetable;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching timetable with Id {Id}", id);
            throw;
        }
    }
}
