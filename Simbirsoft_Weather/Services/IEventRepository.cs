using Simbirsoft_Weather.Models;
using System;

namespace Simbirsoft_Weather.Services
{
    public interface IEventRepository : IDisposable
    {
        Event GetEventById(int id);
        void AddEvent(Event @event);
        void DeleteEventById(int id);
    }
}
