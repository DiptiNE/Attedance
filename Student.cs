using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ParentTeacherBridge.API.Models;

[Table("student")]
public partial class Student
{
    [Key]
    [Column("student_id")]
    public int StudentId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string? Name { get; set; }

    [Required]
    [Column("dob")]
    public DateOnly? Dob { get; set; }

    [Required]
    [StringLength(10)]
    [Column("gender")]
    public string? Gender { get; set; }

    [Required]
    [StringLength(50)]
    [Column("enrollment_no")]
    public string? EnrollmentNo { get; set; }

    [StringLength(10)]
    [Column("blood_group")]
    public string? BloodGroup { get; set; }

    [Required]
    [Column("class_id")]
    public int? ClassId { get; set; }

    [StringLength(255)]
    [Column("profile_photo")]
    public string? ProfilePhoto { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    [JsonIgnore]
    public virtual ICollection<Behaviour> Behaviours { get; set; } = new List<Behaviour>();

    [JsonIgnore]
    public virtual SchoolClass? Class { get; set; }

    [JsonIgnore]
    public virtual ICollection<Performance> Performances { get; set; } = new List<Performance>();

    [JsonIgnore]
    public virtual ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();
}