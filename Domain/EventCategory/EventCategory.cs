namespace TI_BackEnd.Domain.EventCategory
/*
 * This class is used to define an event category
 * An event category is defined by an id, a label and a color
 */
{
    public class EventCategory
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Color { get; set; }

        public EventCategory() { }

        public EventCategory(int id, string label, string color)
        {
            Id = id;
            Label = label;
            Color = color;
        }
    }
}