using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParentTeacherBridge.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEventRepository
{
   

    Task<List<Event>> GetAllAsync();
    Task<Event> GetByIdAsync(int id);
    Task<Event> AddAsync(Event @event);
    Task<bool> UpdateAsync(Event @event);
    Task<bool> DeleteAsync(int id);
}