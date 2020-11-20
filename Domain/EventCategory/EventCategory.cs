namespace TI_BackEnd.Domain.EventCategory
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