using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParentTeacherBridge.API.Models;

public partial class SchoolClass
{
    [Key]
    [Column("class_id")]
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public int? Teacher_Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Teacher? ClassTeacher { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
