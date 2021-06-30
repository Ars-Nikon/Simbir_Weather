using Microsoft.EntityFrameworkCore;
using Simbirsoft_Weather.Models;
using System;
using System.Linq;

namespace Simbirsoft_Weather.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly EventContext _db;

        public EventRepository(DbContextOptions<EventContext> options)
        {
            _db = new EventContext(options);
        }

        public Event GetEventById(int id)
        {
            return _db.Events.SingleOrDefault(e => e.Id == id);
        }

        public void AddEvent(Event @event)
        {
            if (@event == null) return;

            _db.Add(@event);
            _db.SaveChanges();
        }

        public void DeleteEventById(int id)
        {
            var eventInfo = GetEventById(id);

            if (eventInfo == null) return;

            _db.Events.Remove(eventInfo);
            _db.SaveChanges();
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
