using System;
namespace TI_BackEnd.Domain.Event
/*
 * This class is used to define an event
 * An event is defined by an id, an id from an event category, an id from a planning and a label
 */
{
    public class Event
    {
        public int Id { get; set; }
        public int IdEventCategory { get; set; }
        public int IdPlanning { get; set; }
        public string Label { get; set; }

        public string Start { get; set; }
        public string End { get; set; }

        public Event() { }

        public Event(int id, int idEventCategory, int idPlanning, string label, string start, string end)
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