using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParentTeacherBridge.API.DTOs;
using ParentTeacherBridge.API.Models;
using ParentTeacherBridge.API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParentTeacherBridge.API.Controllers
{
    [Route("teacher/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IAttendanceService _attendanceService;
        private readonly IEventService _eventService;
        private readonly ITimetableService _timetableService;
        private readonly IMapper _mapper;




        // private readonly IBehaviourService _behaviourService;

        public TeachersController(ITeacherService teacherService, IAttendanceService attendanceService, IEventService eventService, ITimetableService timetableService,IMapper mapper)
       
        {
            _teacherService = teacherService;
            _attendanceService = attendanceService;
            _eventService = eventService;
            _timetableService = timetableService;
            _mapper = mapper;
        

                }



        #region TeacherCRUD
        // GET: admin/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid teacher ID");

                var teacher = await _teacherService.GetTeacherByIdAsync(id);

                if (teacher == null)
                    return NotFound($"Teacher with ID {id} not found");

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: admin/Teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            try
            {
                if (id <= 0 || teacher == null || id != teacher.TeacherId)
                    return BadRequest("Invalid input data");

                if (string.IsNullOrWhiteSpace(teacher.Name))
                    return BadRequest("Teacher name is required");

                if (string.IsNullOrWhiteSpace(teacher.Email))
                    return BadRequest("Teacher email is required");

                var result = await _teacherService.UpdateTeacherAsync(teacher);

                if (!result)
                    return NotFound($"Teacher with ID {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //// POST: admin/Teachers
        //[HttpPost]
        //public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        //{
        //    try
        //    {
        //        if (teacher == null)
        //            return BadRequest("Teacher data is required");

        //        if (string.IsNullOrWhiteSpace(teacher.Name))
        //            return BadRequest("Teacher name is required");

        //        if (string.IsNullOrWhiteSpace(teacher.Email))
        //            return BadRequest("Teacher email is required");

        //        var result = await _teacherService.CreateTeacherAsync(teacher);

        //        if (!result)
        //            return StatusCode(500, "Failed to create teacher");

        //        return CreatedAtAction(nameof(GetTeacher), new { id = teacher.TeacherId }, teacher);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        // DELETE: admin/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid teacher ID");

                var result = await _teacherService.DeleteTeacherAsync(id);

                if (!result)
                    return NotFound($"Teacher with ID {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region BehaviourCRUD

        //[HttpGet("{teacherId}/behaviour")]
        //public async Task<IActionResult> GetBehaviours(int teacherId)
        //{
        //    var behaviours = await _behaviourService.GetBehavioursByTeacherAsync(teacherId);
        //    if (!behaviours.Any()) return Ok(new List<Behaviour>()); // Empty list instead of 404
        //    return Ok(behaviours);
        //}

        //[HttpGet("{teacherId}/behaviour/{behaviourId}")]
        //public async Task<IActionResult> GetBehaviour(int teacherId, int behaviourId)
        //{
        //    var behaviour = await _behaviourService.GetBehaviourByIdAsync(teacherId, behaviourId);
        //    if (behaviour == null) return NotFound($"Behaviour record not found.");
        //    return Ok(behaviour);
        //}

        //[HttpPost("{teacherId}/behaviour")]
        //public async Task<IActionResult> AddBehaviour(int teacherId, [FromBody] Behaviour behaviour)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var newBehaviour = await _behaviourService.AddBehaviourAsync(teacherId, behaviour);
        //    return CreatedAtAction(nameof(GetBehaviour), new { teacherId, behaviourId = newBehaviour.BehaviourId }, newBehaviour);
        //}

        //[HttpPut("{teacherId}/behaviour/{behaviourId}")]
        //public async Task<IActionResult> UpdateBehaviour(int teacherId, int behaviourId, [FromBody] Behaviour behaviour)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var updated = await _behaviourService.UpdateBehaviourAsync(teacherId, behaviourId, behaviour);
        //    if (updated == null) return NotFound("Behaviour record not found.");
        //    return NoContent();
        //}

        //[HttpDelete("{teacherId}/behaviour/{behaviourId}")]
        //public async Task<IActionResult> DeleteBehaviour(int teacherId, int behaviourId)
        //{
        //    var deleted = await _behaviourService.DeleteBehaviourAsync(teacherId, behaviourId);
        //    if (!deleted) return NotFound("Behaviour record not found.");
        //    return NoContent();
        //} 
        #endregion


        #region AttedanceCRUD
        //AttenadanceCRUD

        // GET: admin/Teachers/{teacherId}/attendance/{studentId}/{id}
        [HttpGet("{teacherId}/attendance/{studentId}/{id}")]
        public async Task<IActionResult> GetAttendance(int teacherId, int studentId, int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null
                || attendance.Class?.Teacher_Id != teacherId
                || attendance.Student_Id != studentId)
                return NotFound($"Attendance record with ID {id} not found for Teacher ID {teacherId} and Student ID {studentId}.");

            var dto = _mapper.Map<AttendanceDto>(attendance);
            return Ok(dto);
        }

        // POST: admin/Teachers/{teacherId}/attendance/{studentId}
        [HttpPost("{teacherId}/attendance/{studentId}")]
        public async Task<IActionResult> CreateAttendance(int teacherId, int studentId, [FromBody] CreateAttendanceDto createDto)
        {
            if (createDto == null)
                return BadRequest("Attendance data is required.");

            var attendance = _mapper.Map<Attendance>(createDto);
            attendance.Student_Id = studentId;
            attendance.CreatedAt = DateTime.UtcNow;

            var created = await _attendanceService.CreateAttendanceAsync(attendance);
            var createdDto = _mapper.Map<AttendanceDto>(created);

            return CreatedAtAction(nameof(GetAttendance),
                new { teacherId, studentId, id = created.Attendance_Id }, createdDto);
        }

        // PUT: admin/Teachers/{teacherId}/attendance/{studentId}/{id}
        [HttpPut("{teacherId}/attendance/{studentId}/{id}")]
        public async Task<IActionResult> UpdateAttendance(int teacherId, int studentId, int id, [FromBody] UpdateAttendanceDto updateDto)
        {
            if (updateDto == null || id != updateDto.AttendanceId)
                return BadRequest("Invalid attendance data.");

            var attendance = _mapper.Map<Attendance>(updateDto);
            attendance.Student_Id = studentId;
            attendance.UpdatedAt = DateTime.UtcNow;

            var updated = await _attendanceService.UpdateAttendanceAsync(attendance);
            if (!updated)
                return NotFound($"Attendance record with ID {id} not found for Teacher ID {teacherId} and Student ID {studentId}.");

            return NoContent();
        }

        // DELETE: admin/Teachers/{teacherId}/attendance/{studentId}/{id}
        [HttpDelete("{teacherId}/attendance/{studentId}/{id}")]
        public async Task<IActionResult> DeleteAttendance(int teacherId, int studentId, int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null
                || attendance.Student_Id != studentId
                || attendance.Class?.Teacher_Id != teacherId)
                return NotFound($"Attendance record with ID {id} not found for Teacher ID {teacherId} and Student ID {studentId}.");

            var deleted = await _attendanceService.DeleteAttendanceAsync(id);
            if (!deleted)
                return NotFound($"Attendance record with ID {id} could not be deleted.");

            return NoContent();
        }


        #endregion

        #region EventsCRUD
        // GET: Teacher/Events
        [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _eventService.Events.ToListAsync();
            return Ok(events);
        }

        // GET: Teacher/Events/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _eventService.Events.FindAsync(id);
            if (@event == null)
                return NotFound();

            return Ok(@event);
        }

        // POST: Teacher/Events
        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event @event)
        {
            @event.CreatedAt = DateTime.UtcNow;
            _eventService.Events.Add(@event);
            await _eventService.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = @event.EventId }, @event);
        }

        // PUT: Teacher/Events/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event @event)
        {
            if (id != @event.EventId)
                return BadRequest();

            @event.UpdatedAt = DateTime.UtcNow;
            _eventService.Entry(@event).State = EntityState.Modified;

            try
            {
                await _eventService.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _eventService.Events.AnyAsync(e => e.EventId == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: Teacher/Events/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _eventService.Events.FindAsync(id);
            if (@event == null)
                return NotFound();

            _eventService.Events.Remove(@event);
            await _eventService.SaveChangesAsync();

            return NoContent();
        }
    

	#endregion

    //// GET: api/timetable
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Timetable>>> GetTimetables()
    //    {
    //        var timetables = await _timetableService.Timetables
    //            .Include(t => t.Class)
    //            .Include(t => t.Subject)
    //            .Include(t => t.Teacher)
    //            .ToListAsync();

    //        return Ok(timetables);
    //    }

    //    // GET: api/timetable/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Timetable>> GetTimetable(int id)
    //    {
    //        var timetable = await _timetableService.Timetables
    //            .Include(t => t.Class)
    //            .Include(t => t.Subject)
    //            .Include(t => t.Teacher)
    //            .FirstOrDefaultAsync(t => t.TimetableId == id);

    //        if (timetable == null)
    //            return NotFound();

    //        return Ok(timetable);
    //    }
    }






}

