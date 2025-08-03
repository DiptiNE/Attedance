using ParentTeacherBridge.API.Models;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;

    public AttendanceService(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<IEnumerable<Attendance>> GetAttendanceByTeacherAndStudentAsync(int teacherId, int studentId)
    {
        return await _attendanceRepository.GetByTeacherAndStudentAsync(teacherId, studentId);
    }

    public async Task<Attendance?> GetAttendanceByIdAsync(int id)
    {
        return await _attendanceRepository.GetByIdAsync(id);
    }

    public async Task<Attendance> CreateAttendanceAsync(Attendance attendance)
    {
        return await _attendanceRepository.AddAsync(attendance);
    }

    public async Task<bool> UpdateAttendanceAsync(Attendance attendance)
    {
        return await _attendanceRepository.UpdateAsync(attendance);
    }

    public async Task<bool> DeleteAttendanceAsync(int id)
    {
        return await _attendanceRepository.DeleteAsync(id);
    }
}
