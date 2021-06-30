using System;

namespace TimerWorker
{
    public class Event
    {
        public int Id { get; set; }
        public string NameEvent { get; set; }
        public string Id_User { get; set; }
        public bool Done { get; set; }
        public string Description { get; set; }
        public string Region { get; set; }
        public DateTime DateEvent { get; set; }
        public DateTime DateSendMessage { get; set; }
    }
}
