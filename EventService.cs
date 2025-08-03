using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParentTeacherBridge.API.Models;
using ParentTeacherBridge.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EventService : IEventService
{
    private readonly IEventRepository _repository;
    public DbSet<Event> Events => _repository.Events;

    public Task<int> SaveChangesAsync() => _repository.SaveChangesAsync();

    public EntityEntry Entry(object entity) => _repository.Entry(entity);


    public EventService(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _repository.Events.ToListAsync();
    }

    public async Task<Event> GetEventByIdAsync(int id)
    {
        return await _repository.Events.FindAsync(id);
    }

    public async Task<Event> CreateEventAsync(Event @event)
    {
        @event.CreatedAt = DateTime.UtcNow;
        _repository.Events.Add(@event);
        await _repository.SaveChangesAsync();
        return @event;
    }

    public async Task<bool> UpdateEventAsync(int id, Event @event)
    {
        if (id != @event.EventId)
            return false;

        @event.UpdatedAt = DateTime.UtcNow;
        _repository.Entry(@event).State = EntityState.Modified;

        try
        {
            await _repository.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_repository.Events.Any(e => e.EventId == id))
                return false;

            throw;
        }
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        var @event = await _repository.Events.FindAsync(id);

        if (@event == null)
            return false;

        _repository.Events.Remove(@event);
        await _repository.SaveChangesAsync();
        return true;
    }
}