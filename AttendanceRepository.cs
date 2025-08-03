using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParentTeacherBridge.API.Data;
using ParentTeacherBridge.API.Models;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly ParentTeacherBridgeAPIContext _context;
    private readonly ILogger<AttendanceRepository> _logger;

    public AttendanceRepository(ParentTeacherBridgeAPIContext context, ILogger<AttendanceRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Attendance>> GetByTeacherAndStudentAsync(int teacherId, int studentId)
    {
        _logger.LogInformation("Fetching attendance for TeacherId: {TeacherId}, StudentId: {StudentId}", teacherId, studentId);

        var attendances = await _context.Attendance
            .Include(a => a.Class)
            .Where(a => a.Student_Id == studentId && a.Class.Teacher_Id == teacherId)
            .ToListAsync();

        _logger.LogInformation("Fetched {Count} attendance records.", attendances.Count);
        return attendances;
    }

    public async Task<Attendance?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching attendance by Id: {Id}", id);

        var attendance = await _context.Attendance
            .Include(a => a.Class)
            .FirstOrDefaultAsync(a => a.Attendance_Id == id);

        if (attendance == null)
            _logger.LogWarning("No attendance record found with Id: {Id}", id);
        else
            _logger.LogInformation("Attendance record found for Id: {Id}", id);

        return attendance;
    }

    public async Task<Attendance> AddAsync(Attendance attendance)
    {
        _logger.LogInformation("Adding new attendance record for StudentId: {StudentId}, ClassId: {ClassId}", attendance.Student_Id, attendance.Class_Id);

        _context.Attendance.Add(attendance);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Attendance record added successfully with Id: {Id}", attendance.Attendance_Id);
        return attendance;
    }

    public async Task<bool> UpdateAsync(Attendance attendance)
    {
        _logger.LogInformation("Updating attendance record Id: {Id}", attendance.Attendance_Id);

        _context.Entry(attendance).State = EntityState.Modified;
        var updated = await _context.SaveChangesAsync() > 0;

        if (updated)
            _logger.LogInformation("Attendance record Id: {Id} updated successfully.", attendance.Attendance_Id);
        else
            _logger.LogWarning("Attendance record Id: {Id} update failed.", attendance.Attendance_Id);

        return updated;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Deleting attendance record Id: {Id}", id);

        var attendance = await _context.Attendance.FindAsync(id);
        if (attendance == null)
        {
            _logger.LogWarning("Attendance record Id: {Id} not found for deletion.", id);
            return false;
        }

        _context.Attendance.Remove(attendance);
        var deleted = await _context.SaveChangesAsync() > 0;

        if (deleted)
            _logger.LogInformation("Attendance record Id: {Id} deleted successfully.", id);
        else
            _logger.LogWarning("Attendance record Id: {Id} deletion failed.", id);

        return deleted;
    }
}
