using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParentTeacherBridge.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEventService
{
    DbSet<Event> Events { get; }
    Task<int> SaveChangesAsync();
    EntityEntry Entry(object entity);
    Task<List<Event>> GetAllEventsAsync();
    Task<Event> GetEventByIdAsync(int id);
    Task<Event> CreateEventAsync(Event @event);
    Task<bool> UpdateEventAsync(int id, Event @event);
    Task<bool> DeleteEventAsync(int id);
}