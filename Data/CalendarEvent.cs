namespace MyMedCalendar.Data
{
    public class CalendarEvent : BaseEntity
    {
        public int Id { get; set; }

        public DateTime EventDateTime { get; set; }
        public string Title { get; set; } = null!; 
        public string Notes { get; set; } = null!; // Optional notes about the event
        public bool IsCompleted { get; set; } = false;

        //FK
        public int CalendarId { get; set; }
        public virtual Calendar Calendar { get; set; } = null!;

        public int? MedicationEventId { get; set; }
        public virtual MedicationEvent? MedicationEvent { get; set; }
    }
}
