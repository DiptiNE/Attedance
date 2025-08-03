using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ParentTeacherBridge.API.Models;
[Table("teacher")]
public partial class Teacher
{
    [Key]
    [Column("teacher_id")]
    public int TeacherId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name can't exceed 100 characters")]
    [Column("name")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(150)]
    [Column("email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    [Column("password")]
    public string? Password { get; set; }

    [Phone(ErrorMessage = "Invalid phone number")]
    [StringLength(15)]
    [Column("phone")]
    public string? Phone { get; set; }

    [StringLength(10)]
    [Column("gender")]
    public string? Gender { get; set; }

    [Url(ErrorMessage = "Invalid URL format for photo")]
    [Column("photo")]
    public string? Photo { get; set; }

    [StringLength(100)]
    [Column("qualification")]
    public string? Qualification { get; set; }

    [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years")]

    [Column("experience_years")]
    public int? Experience_Years { get; set; }
    [Column("is_active")]
    public bool? Is_Active { get; set; }
    [Column("created_at")]
    public DateTime? Created_At { get; set; }
    [Column("updated_at")]
    public DateTime? Updated_At { get; set; }

    [JsonIgnore]
    public virtual ICollection<Behaviour> Behaviours { get; set; } = new List<Behaviour>();

    [JsonIgnore]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [JsonIgnore]
    public virtual ICollection<Performance> Performances { get; set; } = new List<Performance>();

    [JsonIgnore]
    public virtual ICollection<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();

    [JsonIgnore]
    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}