using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ParentTeacherBridge.API.Models;

[Table("events")]
public partial class Event
{
    [Key]
    [Column("event_id")]
    public int EventId { get; set; }

    [Column("title")]
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
    public string? Title { get; set; }

    [Column("desciption")]
    [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
    public string? Description { get; set; }

    [Column("event_date")]
    public DateOnly? EventDate { get; set; }

    [Column("start_time")]
    public TimeOnly? StartTime { get; set; }

    [Column("end_time")]
    public TimeOnly? EndTime { get; set; }

    [Column("venue")]
    [StringLength(200, ErrorMessage = "Venue can't be longer than 200 characters.")]
    public string? Venue { get; set; }

    [Column("event_type")]
    [Required(ErrorMessage = "Event type is required.")]
    [StringLength(50, ErrorMessage = "Event type can't be longer than 50 characters.")]
    public string? EventType { get; set; }

    [Column("teacher_id")]
    [Range(1, int.MaxValue, ErrorMessage = "Teacher ID must be a positive integer.")]
    public int? TeacherId { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    public virtual Teacher? Teacher { get; set; }
}