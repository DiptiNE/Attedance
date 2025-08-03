using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParentTeacherBridge.API.Data;
using ParentTeacherBridge.API.Models;
using ParentTeacherBridge.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EventRepository : IEventRepository
{
    private readonly ParentTeacherBridgeAPIContext _context;
    private readonly ILogger<EventRepository> _logger;

    public EventRepository(ParentTeacherBridgeAPIContext context, ILogger<EventRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Event>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all events from the database.");
        return await _context.Events.ToListAsync();
    }

    public async Task<Event> GetByIdAsync(int id)
    {
        _logger.LogInformation("Searching for event with ID {EventId}.", id);
        return await _context.Events.FindAsync(id);
    }

    public async Task<Event> AddAsync(Event @event)
    {
        _logger.LogInformation("Adding new event to the database.");
        @event.CreatedAt = DateTime.UtcNow;
        _context.Events.Add(@event);
        await _context.SaveChangesAsync();
        return @event;
    }

    public async Task<bool> UpdateAsync(Event @event)
    {
        _logger.LogInformation("Updating event with ID {EventId}.", @event.EventId);
        @event.UpdatedAt = DateTime.UtcNow;
        _context.Entry(@event).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            _logger.LogWarning("Concurrency issue during update of event ID {EventId}.", @event.EventId);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var @event = await _context.Events.FindAsync(id);
        if (@event == null)
        {
            _logger.LogWarning("Delete failed: Event with ID {EventId} not found.", id);
            return false;
        }

        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Event with ID {EventId} successfully deleted.", id);
        return true;
    }
}