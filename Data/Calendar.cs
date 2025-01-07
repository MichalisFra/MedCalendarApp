namespace MyMedCalendar.Data
{
    public class Calendar : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        //FK
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;

        //Navigation
        public virtual ICollection<CalendarEvent> Events { get; set; } = new List<CalendarEvent>();

    }
}
