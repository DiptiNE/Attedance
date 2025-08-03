using ParentTeacherBridge.API.Models;

public interface IAttendanceRepository
{
    Task<IEnumerable<Attendance>> GetByTeacherAndStudentAsync(int teacherId, int studentId);
    Task<Attendance?> GetByIdAsync(int id);
    Task<Attendance> AddAsync(Attendance attendance);
    Task<bool> UpdateAsync(Attendance attendance);
    Task<bool> DeleteAsync(int id);
}
