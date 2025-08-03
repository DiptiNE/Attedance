using ParentTeacherBridge.API.Models;

public interface IAttendanceService
{
    Task<IEnumerable<Attendance>> GetAttendanceByTeacherAndStudentAsync(int teacherId, int studentId);
    Task<Attendance?> GetAttendanceByIdAsync(int id);
    Task<Attendance> CreateAttendanceAsync(Attendance attendance);
    Task<bool> UpdateAttendanceAsync(Attendance attendance);
    Task<bool> DeleteAttendanceAsync(int id);
}
