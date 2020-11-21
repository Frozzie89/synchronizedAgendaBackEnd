using System;
namespace TI_BackEnd.Domain.Event
{
    public class Event
    {
        public int Id { get; set; }
        public int IdEventCategory { get; set; }
        public int IdPlanning { get; set; }
        public string Label { get; set; }

        // format : "dd/MM/yyyy HH:mm"
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Event() { }

        public Event(int id, int idEventCategory, int idPlanning, string label, DateTime start, DateTime end)
        {
            Id = id;
            IdEventCategory = idEventCategory;
            IdPlanning = idPlanning;
            Label = label;
            Start = start;
            End = end;
        }
    }
}