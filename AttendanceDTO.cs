using System;

namespace ParentTeacherBridge.API.DTOs
{
    /// <summary>
    /// DTO used for returning attendance data to clients
    /// </summary>
    public class AttendanceDto
    {
        public int Attendance_Id { get; set; }
        public int Student_Id { get; set; }
        public int Class_Id { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remark { get; set; }
        public TimeOnly? Marked_Time { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }

    /// <summary>
    /// DTO used when creating a new attendance record (POST)
    /// </summary>
    public class CreateAttendanceDto
    {
        public int Class_Id { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remark { get; set; }
        public TimeOnly? Marked_Time { get; set; }
    }

    /// <summary>
    /// DTO used when updating an existing attendance record (PUT)
    /// </summary>
    public class UpdateAttendanceDto
    {
        public int Attendance_Id { get; set; }
        public int Class_Id { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remark { get; set; }
        public TimeOnly? Marked_Time { get; set; }
    }
}
